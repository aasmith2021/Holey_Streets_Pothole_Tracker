<template>
  <div id="app">
    <div>
      <b-nav class="nav">
        <div class="logo-and-name">
          <a href="http://localhost:8080/"
            ><img
              class="logo-image"
              src="../public/pothole.svg"
              alt="Holey Streets Logo"
          /></a>
          <router-link v-bind:to="{ name: 'home' }" class="holey-streets">
            HOLEY STREETS
          </router-link>
        </div>
        <dropdown />
      </b-nav>
    </div>
    <router-view />
  </div>
</template>

<script>
import Dropdown from "./views/UserDropdown.vue";
import potholeService from "./services/PotholeService.js";

export default {
  name: "app",
  components: {
    Dropdown,
  },
  created() {
    this.setPotholes()
  },
  methods: {
    setPotholes(){
      potholeService.list().then((r) => {
      r.data.forEach((p) => {
        this.$set(p, "isBig", false);
        this.$set(p, "opacity", 1);
      });
      this.$store.commit("SET_POTHOLE_LIST", r.data);
    });
    }
  }
};
</script>


<style>
.nav {
  display: flex;
  background-color: #adc178;
  height: 70px;
  width: 100%;
  align-items: center;
  justify-content: space-between;
}

.holey-streets {
  text-decoration: none;
  color: black;
  font-size: 25px;
  margin: 0px 10px;
  font-family: "Luckiest Guy", cursive;
}

.logo-image {
  height: 25px;
  width: auto;
  margin-left: 15px;
  margin-bottom: 11px;
}

div.title-and-pictures {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 15px 0 0 0;
}

@media only screen and (min-width: 640px) {
  .tire-pic {
    height: 60px;
    width: auto;
  }

  div.subtitle-box {
    border-bottom: 3px solid #adc178;
    border-top: 3px solid #adc178;
    margin: 15px 0 10px 0;
    height: 3rem;
    display: flex;
    align-items: center;
    justify-content: center;
  }
}

@media only screen and (max-width: 640px) {
  .tire-pic {
    height: 60px;
    width: 25vw;
  }

  div.subtitle-box {
    height: 3rem;
  }
}

@media only screen and (max-width: 605px) {
  .tire-pic {
    display: none;
  }

  div.subtitle-box {
    height: 3rem;
  }
}

@media only screen and (max-width: 326px) {
  div.subtitle-box {
    height: 4.2rem;
  }
}

div.subtitle-box {
  border-bottom: 3px solid #adc178;
  border-top: 3px solid #adc178;
  margin: 15px 0 10px 0;
  display: flex;
  align-items: center;
  justify-content: center;
}

h2.subtitle {
  font-family: "Luckiest Guy", cursive;
  font-size: 22px;
  text-align: center;
  margin: 0;
}

.title {
  font-family: "Luckiest Guy", cursive;
  font-size: 34px;
  white-space: nowrap;
  margin: 0;
  padding: 15px 10px;
  border-radius: 20px;
  color: black;
}

p.subtitle-claim-form {
  font-size: 1rem;
  text-align: center;
  margin: 0;
}

div.subtitle-box-claim-form {
  border-bottom: 5px solid #adc178;
  border-top: 5px solid #adc178;
  margin: 15px 0 10px 0;
  align-items: center;
  justify-content: center;
}
button{
  border: 1px solid black;
  border-radius: 5px;
}
</style>
