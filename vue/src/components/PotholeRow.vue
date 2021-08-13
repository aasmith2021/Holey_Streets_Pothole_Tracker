<template>
  <tr @mouseover="mouseOn(pothole.id)" @mouseleave="mouseOff(pothole.id)">
    <td>{{ currentPothole.id }}</td>
    <td>{{ currentPothole.latitude }}</td>
    <td>{{ currentPothole.longitude }}</td>
    <td>
      <img
        :src="currentPothole.imageLink"
        style="height: 75px"
        target="_blank"
      />
    </td>
    <td>{{ truncateReportedDate }}</td>
    <td>{{ currentPothole.reportingUserId }}</td>
    <td>
      <b-form-datepicker
        :min="min"
        v-if="isEditClicked"
        v-model="newPothole.inspectedDate"
        label-no-date-selected=" "
        size="sm"
      />
      <br v-if="isEditClicked" />
      <div v-if="!isEditClicked">{{ truncateInspectedDate }}</div>
    </td>
    <td>
      <b-form-datepicker
        :min="min"
        v-if="isEditClicked"
        v-model="newPothole.repairedDate"
        label-no-date-selected=" "
        size="sm"
      />
      <div v-if="!isEditClicked">{{ truncateRepairedDate }}</div>
    </td>
    <td>
      <select
        name="Reported"
        v-if="isEditClicked"
        v-model="newPothole.repairStatus"
      >
        <option value="Reported">Reported</option>
        <option value="Inspected">Inspected</option>
        <option value="Repaired">Repaired</option>
      </select>
      <div v-if="!isEditClicked">{{ currentPothole.repairStatus }}</div>
    </td>
    <td>
      <input
        type="range"
        min="1"
        max="3"
        v-if="isEditClicked"
        v-model.number="newPothole.severity"
      />
      <div v-if="!isEditClicked">{{ currentPothole.severity }}</div>
    </td>
    <td class="edit-delete-td">
      <a
        href
        class="edit-delete"
        v-if="!isEditClicked"
        v-on:click.prevent="editThisPothole()"
        ><img src="/pencil.ico" class="ico" /> {{ "Edit" }}</a
      ><br v-if="!isEditClicked" />
      <a
        href
        class="edit-delete"
        v-if="isEditClicked"
        v-on:click.prevent="discardChanges()"
        ><img src="/no.ico" class="ico" /> {{ "Discard Changes" }}</a
      ><br v-if="isEditClicked" />
      <div class="button-spacer"></div>
      <a
        href
        class="edit-delete"
        v-if="isEditClicked"
        v-on:click.prevent="updatePothole()"
        ><img src="/save.ico" class="ico" /> Save</a
      >
      <div class="delete-button" v-if="!isEditClicked">
        <delete-confirmation v-bind:pothole="pothole" />
      </div>
    </td>
  </tr>
</template>

<script>
import deleteConfirmation from "../components/DeleteConfirmation.vue";
import potholeService from "../services/PotholeService.js";

export default {
  name: "pothole-row",
  props: ["pothole"],
  components: {
    deleteConfirmation,
  },
  data() {
    const now = new Date();
    const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const minDate = new Date(today);
    return {
      isEditClicked: false,
      newPothole: {},
      currentPothole: this.pothole,
      min: minDate,
    };
  },
  methods: {
    formatDate(date) {
      if (date != null) {
        const month = date.substring(5, 7);
        const day = date.substring(8, 10);
        const year = date.substring(0, 4);
        date = month + "-" + day + "-" + year;
      }
      return date;
    },
    updatePothole() {
      this.endEdit(this.pothole.id);
      potholeService
        .updatePothole(this.newPothole)
        .then(() => {
          this.currentPothole = this.newPothole;
          this.$store.commit("UPDATE_POTHOLE", this.newPothole);
          this.newPothole = {};
          this.isEditClicked = !this.isEditClicked;
          this.setPotholes();
        })
        .catch((e) => {
          console.log(e.status);
        });
    },
    editThisPothole() {
      this.startEdit(this.pothole.id);

      Object.assign(this.newPothole, this.pothole);

      this.isEditClicked = !this.isEditClicked;
    },
    setPotholes() {
      potholeService.list().then((r) => {
        r.data.forEach((p) => {
          this.$set(p, "isBig", false);
          this.$set(p, "opacity", 1);
        });
        this.$store.commit("SET_POTHOLE_LIST", r.data);
      });
    },
    discardChanges() {
      this.endEdit(this.pothole.id);
      this.isEditClicked = !this.isEditClicked;
      this.newPothole = {};
    },
    mouseOn(id) {
      if (!this.isEditClicked) {
        this.$emit("mouse-on-tr", id);
      }
    },
    mouseOff(id) {
      if (!this.isEditClicked) {
        this.$emit("mouse-off-tr", id);
      }
    },
    startEdit(id) {
      this.$emit("start-edit", id);
    },
    endEdit(id) {
      this.$emit("end-edit", id);
    },
    getCurrentDate() {
      var today = new Date();
      var dd = today.getDate();
      var mm = today.getMonth() + 1;
      var yyyy = today.getFullYear();

      today = yyyy + "-" + mm + "-" + dd;
      document.getElementById("datefield").setAttribute("max", today);
    },
  },
  computed: {
    truncateReportedDate() {
      return this.formatDate(this.currentPothole.reportedDate);
    },
    truncateInspectedDate() {
      return this.formatDate(this.currentPothole.inspectedDate);
    },
    truncateRepairedDate() {
      return this.formatDate(this.currentPothole.repairedDate);
    },
  },
};
</script>

<style scoped>
td {
  font-size: 12px;
}

div.button-spacer {
  height: 15px;
}

.edit-delete-td {
  text-align: left;
}
</style>