<template>
  <div id="ourmap" v-bind:class="{ 'smaller-size-map': smallComp }">
    <l-map
      v-if="showMap"
      :zoom="zoom"
      :center="center"
      :options="mapOptions"
      :max-bounds="bounds"
      class="brown-border"
      @update:center="centerUpdate"
      @update:zoom="zoomUpdate"
      ref="myMap"
    >
      <l-control position="bottomright">
        <div v-show="mapKey" id="key-div" class="brown-border">
          <div v-for="k in mapKey" v-bind:key="k.icon">
            <img :src="k.icon" class="key-icon" alt="" />{{ k.name }}
          </div>
        </div>
      </l-control>
      <l-control-scale position="bottomleft" :imperial="true" :metric="false" />

      <l-tile-layer :url="url" :attribution="attribution" />
      <l-marker
        v-for="pothole in markers"
        v-bind:key="pothole.id"
        :lat-lng="makeLatLng(pothole.latitude, pothole.longitude)"
        :opacity="pothole.opacity"
        :icon="makeIcon(pothole)"
      >
        <l-popup>
          <img
            v-show="pothole.imageLink"
            :src="pothole.imageLink"
            class="popup-image brown-border"
            alt=""
          /><br />
          <div style="text-align: center">
            <div><b>Lat:</b> {{ pothole.latitude }}</div>
            <div><b>Lng:</b> {{ pothole.longitude }}</div>
            <div><b>Reported:</b> {{ formatDate(pothole.reportedDate) }}</div>
            <div v-show="pothole.inspectedDate">
              <b> Inspected:</b> {{ formatDate(pothole.inspectedDate) }}
            </div>
            <div v-show="pothole.repairedDate">
              <b> Repaired:</b> {{ formatDate(pothole.repairedDate) }}
            </div>
            <div><b>Status:</b> {{ pothole.repairStatus }}</div>
            <div v-show="pothole.severity" :style="{ color: getSevColor(pothole.severity)}">
              <b>Severity:</b> {{ getSevText(pothole.severity) }}
            </div>
            <div>
              <b>ID: {{ pothole.id }}</b>
            </div>
          </div>
        </l-popup>
      </l-marker>
    </l-map>
    <div v-show="latLongZoomInfoVisible">
      {{ currentCenter }} {{ currentZoom }}
    </div>
  </div>
</template>

<script>
import { latLngBounds, latLng } from "leaflet";
import L from "leaflet";
import {
  LMap,
  LTileLayer,
  LMarker,
  LPopup,
  LControl,
  LControlScale,
} from "vue2-leaflet";
export default {
  name: "Map",
  props: ["currentView", "latLongZoomInfoVisible", "mapKey", "isBigMap"],
  components: {
    LMap,
    LTileLayer,
    LMarker,
    LPopup,
    LControl,
    LControlScale,
  },
  watch: {
    currentCenter: function () {
      return this.$emit("sendupcoords", this.currentCenter);
    },
  },
  data() {
    return {
      zoom: 13,
      center: latLng(39.12859669871998, -84.50453381147379),
      url: "https://tile.thunderforest.com/atlas/{z}/{x}/{y}.png?apikey=68585e9a3c4f46788036058c0acfaa4e",
      attribution: this.currentCenter,
      currentZoom: 13,
      currentCenter: latLng(39.12859669871998, -84.50453381147379),
      showParagraph: false,
      bounds: latLngBounds([
        [14.558205167291081, -185.09765625],
        [53.27992461623911, -31.093255571223526],
      ]),
      mapOptions: {
        zoomSnap: 0.5,
      },
      showMap: true,
      regSize: [25, 41],
      bigSize: [37, 61],
      icons: {
        baseIcon: "marker-icon-blue.png",
        reportIcon: "marker-icon-red.png",
        inspectIcon: "marker-icon-yellow.png",
        repairIcon: "marker-icon-green.png",
        userIcon: "marker-icon-gold.png",
        nonUserIcon: "marker-icon-grey.png",
      },
    };
  },
  methods: {
    makeIcon(p) {
      const size = p.isBig ? this.bigSize : this.regSize;
      const url = this.getColor(p);
      return L.icon({
        iconUrl: url,
        iconSize: size,
        iconAnchor: this.dynamicAnchor(size),
      });
    },
    getColor(p) {
      let rColor = this.icons.baseIcon;
      if (this.currentView == "employee" || this.currentView == "home") {
        switch (p.repairStatus) {
          case "Reported":
            rColor = this.icons.reportIcon;
            p.opacity = 1;
            break;
          case "Inspected":
            rColor = this.icons.inspectIcon;
            p.opacity = 1;
            break;
          case "Repaired":
            rColor = this.icons.repairIcon;
            p.opacity = 0.5;
            break;
        }
      }
      if (this.currentView == "account") {
        if (p.reportingUserId == this.$store.state.user.userId) {
          rColor = this.icons.userIcon;
          p.opacity = 1;
        } else {
          rColor = this.icons.nonUserIcon;
          p.opacity = 0.5;
        }
      }
      return rColor;
    },
    redrawSize() {
      setTimeout(() => {
        this.$refs.myMap.mapObject.invalidateSize();
      }, 100);
    },
    dynamicAnchor(s) {
      return [s[0] / 2, s[1]];
    },
    zoomUpdate(zoom) {
      this.currentZoom = zoom;
    },
    centerUpdate(center) {
      this.currentCenter = center;
    },
    showLongText() {
      this.showParagraph = !this.showParagraph;
    },
    innerClick() {
      alert("Click!");
    },
    startAdd(p) {
      this.markers.push(p);
    },
    makeLatLng(lat, lng) {
      return latLng(lat, lng);
    },
    formatDate(d) {
      if (d) {
        return d.substring(0, 10);
      }
    },
    getSevText(i) {
      switch (i) {
        case 1:
          return "Low";
        case 2:
          return "Medium";
        case 3:
          return "High";
      }
    },
    getSevColor(i) {
      switch (i) {
        case 1:
          return "green";
        case 2:
          return "orange";
        case 3:
          return "red";
      }
    },
  },
  computed: {
    smallComp() {
      if (this.isBigMap || this.currentView == "home") {
        return false;
      } else {
        return true;
      }
    },
    markers() {
      return this.$store.state.potholes;
    },
  },
  created() {
    return this.$emit("sendupcoords", this.currentCenter);
  },
};
</script>

<style scoped>

#ourmap {
  height: 70vh;
  width: 95vw;
}

@media only screen and (min-width: 1024px) {
  #ourmap.smaller-size-map {
    height: 72vh;
    width: 30vw;
  }
}

@media only screen and (max-width: 1024px) {
  #ourmap.smaller-size-map {
    height: 30vh;
    width: 95vw;
  }
}
.brown-border {
  border: 1px solid #6c584c;
  border-radius: 5px;
}
.key-icon {
  height: 20px;
}
#key-div {
  background-color: rgba(255, 255, 240, 0.8);
  padding: 5px;
}
.popup-image {
  height: 150px;
}
</style>