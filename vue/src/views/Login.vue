<template>
  <div id="login" class="text-center">
    <div class="login-form-background">
      <form class="form-signin" @submit.prevent="login">
        <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
        <router-link :to="{ name: 'register' }">Need an account?</router-link><br>
        <div
          class="alert alert-danger"
          role="alert"
          v-if="invalidCredentials"
        >Invalid username and password!</div>
        <div
          class="alert alert-success"
          role="alert"
          v-if="this.$route.query.registration"
        >Thank you for registering, please sign in.</div><br>
        <label for="username" class="sr-only">Username</label>
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus
        /><br>
        <label for="password" class="sr-only">Password</label>
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required
        /><br/>
        <button class="button" type="submit">Sign in</button>
      </form>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped>
@media only screen and (min-width: 1189px) {
  #login {
    height: 75vh;
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 8vh;
    padding: 10vh;
    border-radius: 10px;
    background-image: url("../../public/road_pic_3.jpg");
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
  }
}

@media only screen and (max-width: 1189px) {
  #login {
    height: 75vh;
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 8vh;
    padding: 10vh;
    border-radius: 10px;
    background-image: url("../../public/road_pic_3.jpg");
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center;
  }
}

button {
  background-color: #adc178;
  border: none;
  padding: 15px 32px;
  border-radius: 5px;
}

.button:active {
  background-color: lightblue;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}

.login-form-background {
  display:flex;
  border: 2px solid black;
  border-radius: 10px;
  padding: 2vh;
  background-color: #f0ead2;
}

</style>
