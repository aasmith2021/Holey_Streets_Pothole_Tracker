<template>
  <div id="register" class="text-center">
    <div class="register-form-background">
      <form class="form-register" @submit.prevent="register">
        <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
        <router-link :to="{ name: 'login' }">Have an account?</router-link><br>
        <div class="alert alert-danger" role="alert" v-if="registrationErrors">
          {{ registrationErrorMsg }}
        </div><br>
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
        /><br>
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          placeholder="Confirm Password"
          v-model="user.confirmPassword"
          required
        />
      <br>
        <button class="button" type="submit">
          Create Account
        </button>
    
      </form>
    </div>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style scoped>
@media only screen and (min-width: 1189px) {
  #register {
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
  #register {
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

.register-form-background {
  display:flex;
  border: 2px solid black;
  border-radius: 10px;
  padding: 2vh;
  background-color: #f0ead2;
}

</style>
