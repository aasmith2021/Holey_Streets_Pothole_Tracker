<template>
  <div>
    <div class="title-and-pictures">
      <img
        class="left-pic tire-pic"
        src="../../public/tire_track_left.jpg"
        alt="Tire track picture"
      />
      <h1 class="centered-h1 title">
        Employee <br class="small-screen-line-break" />Dashboard
      </h1>
      <img
        class="right-pic tire-pic"
        src="../../public/tire_track_right.jpg"
        alt="Tire track picture"
      />
    </div>

    <div id="map" class="map-and-table-container">
      <div class="map-div">
        <admin-map
          v-bind:isBigMap="isExpandClicked"
          ref="streetmap"
          currentView="employee"
          v-bind:mapKey="employeeKey"
          v-bind:latLongZoomInfoVisible="false"
        />
        <button class="resize-button" v-on:click="resizeMap()">
          {{ !isExpandClicked ? "Expand" : "Minimize" }}
        </button>
      </div>

      <div class="hole-list-table">
        <editable-hole-list
          :potholes="$store.state.potholes"
          v-on:mouse-on-tr="mouseOn"
          v-on:mouse-off-tr="mouseOff"
          v-on:start-edit="startEdit"
          v-on:end-edit="endEdit"
        />
      </div>
    </div>
  </div>
</template>
<!--should display list of holes, rank severity(SLIDER!), add inspection/repair date, change status, delete-->

<script>
import EditableHoleList from "../components/EditableHoleList.vue";
import AdminMap from "../components/StreetMap.vue";
export default {
  components: {
    EditableHoleList,
    AdminMap,
  },
  data() {
    return {
      isExpandClicked: false,
      employeeKey: [
        { icon: "marker-icon-red.png", name: "Reported" },
        { icon: "marker-icon-yellow.png", name: "Inspected" },
        { icon: "marker-icon-green.png", name: "Repaired" },
      ],
      emColor: "marker-icon-violet.png",
    };
  },
  methods: {
    resizeMap() {
      var element = document.getElementById("map");
      element.classList.toggle("expand-map");
      this.isExpandClicked = !this.isExpandClicked;
      this.$refs.streetmap.redrawSize();
    },
    findPothole(id) {
      return this.$store.state.potholes.find((p) => p.id == id);
    },
    mouseOn(id) {
      this.$set(this.findPothole(id), "isBig", true);
    },
    mouseOff(id) {
      this.$set(this.findPothole(id), "isBig", false);
    },
    startEdit(id) {
      let p = this.findPothole(id);
      this.$set(p, "isBig", true);
    },
    endEdit(id) {
      let p = this.findPothole(id);
      this.$set(p, "isBig", false);
    },
  },
};
</script>

<style scoped>
@media only screen and (min-width: 1024px) {
  div.map-and-table-container {
    display: flex;
    flex-direction: row-reverse;
    gap: 0.5vw;
    align-items: flex-start;
    justify-content: center;
  }

  .small-screen-line-break {
    display: none;
  }

  div.expand-map {
    display: flex;
    flex-direction: column;
    gap: 0.5vw;
    align-items: center;
    justify-content: center;
  }
}

@media only screen and (max-width: 1024px) {
  div.map-and-table-container {
    display: flex;
    flex-direction: column;
    gap: 0.5vw;
    align-items: center;
    justify-content: center;
  }

  .small-screen-line-break {
    display: none;
  }

  button.resize-button {
    display: none;
  }
}

@media only screen and (max-width: 412px) {
  .small-screen-line-break {
    display: inline;
  }
}

div.map-div {
  margin-top: 15px;
}

button.resize-button {
  font-size: 16px;
  font-weight: bold;
  background-color: #adc178;
  width: 175px;
  border: 1px solid black;
  border-radius: 5px;
}
</style>