<template>
  <div class="form">
    <b-button
      v-b-toggle.sidebar-1
      class="add-new"
      v-if="$store.state.token != ''"
      >Add A Pothole</b-button
    >
    <b-sidebar
      id="sidebar-1"
      sidebar-class="border-right border-dark"
      text-variant="white"
      bg-variant="secondary"
      title="New Pothole"
      no-slide
      @shown="startAdd"
      @hidden="endAdd"
      width="250px"
    >
      <div class="px-4 pb-2">
        <form class="add-pothole" v-if="$store.state.token != ''">
          <label for="latitude">Latitude:</label><br />
          <input
            type="text"
            id="latitude"
            name="latitude"
            v-model.number="pothole.latitude"
          /><br class="add-pothole-form-break" /><br class="add-pothole-form-break" />
          <label for="longitude">Longitude:</label><br />
          <input
            type="text"
            id="longitude"
            name="longitude"
            v-model.number="pothole.longitude"
          /><br class="add-pothole-form-break" /><br class="add-pothole-form-break" />
          <label for="image-link">Image Link:</label><br />
          <input
            type="url"
            id="image-link"
            name="image-link"
            v-model="pothole.imageLink"
          /><br class="add-pothole-form-break"  /><br class="add-pothole-form-break" />
          <button class="add-button" type="button" v-b-toggle.sidebar-1 v-on:click="addNewPothole">
            Add New Pothole
          </button>
        </form>
      </div>
    </b-sidebar>
  </div>
</template>

<script>
import potholeService from "@/services/PotholeService.js";

export default {
  name: "add-pothole",
  props: ["currentCenter"],
  data() {
    return {
      pothole: {
        latitude: this.currentCenter.lat,
        longitude: this.currentCenter.lng,
        imageLink: "",
      },
      variant: "",
    };
  },
  watch: {
    currentCenter: function () {
      this.pothole.latitude = this.currentCenter.lat;
      this.pothole.longitude = this.currentCenter.lng;
    },
  },
  methods: {
    addNewPothole() {

      potholeService.addPothole(this.pothole).then((r) => {
        if (r.status === 200) {
          this.setPotholes();
        }
        this.resetPothole();
      });
    },
    resetPothole() {
      this.pothole.imageLink = "";
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
    startAdd(){
      this.$emit("start-add")
    }
    ,endAdd(){
      this.$emit("end-add")
    }
  },
};
</script>

<style scoped>
button.add-new {
  color: white;
  font-size: 20px;
  background-color: #51616b;
  font-family: "Luckiest Guy", cursive;
  position: relative;
  left: 2.5vw;
  border-radius: 5px;
}

button.add-new:hover {
  color: #adc178;
}

button.add-button {
  color: black;
  font-family: "Luckiest Guy", cursive;
  font-size: 16px;
  background-color: #adc178;
  width: 175px;
  border: 1px solid black;
  border-radius: 5px;
}

div.form {
  height: 0;
}
</style>