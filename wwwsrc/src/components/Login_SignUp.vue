<template>
  <div class="login">
    <navbar></navbar>
    <div class="login-page">
      <div v-if="sigup == false">
        <form @submit.prevent="login()">
          <div class="form-group">
            <label>Email</label>
            <input type="email" v-model="loginUser.email" class="form-control" placeholder="Email">
          </div>
          <div class="form-group">
            <label>Password</label>
            <input type="password" v-model="loginUser.password" class="form-control" placeholder="Password">
          </div>
          <div class="form-group">
            <button type="submit" class="btn btn-outline-success btn-block">Login</button>
          </div>
        </form>
        <button @click="sigup = true" class="btn btn-outline-secondary btn-block">Sign Up</button>
      </div>
      <div v-if="sigup == true">
        <form @submit.prevent="signUp()">
          <div class="form-group">
            <label>Username</label>
            <input type="text" v-model="createdUser.name" class="form-control" placeholder="Username">
          </div>
          <div class="form-group">
            <label>Email</label>
            <input type="email" v-model="createdUser.email" class="form-control" placeholder="Email">
          </div>
          <div class="form-group">
            <label>Password</label>
            <input type="password" v-model="createdUser.password" class="form-control" placeholder="Password">
          </div>
          <div class="form-group">
            <button type="submit" class="btn btn-outline-success btn-block">Sign Up</button>
          </div>
        </form>
        <button @click="sigup = false" class="btn btn-outline-secondary btn-block">Login</button>
      </div>
    </div>
  </div>
</template>

<script>
  import Navbar from './Navbar.vue'
  export default {
    name: 'Login_SignUp',
    data() {
      return {
        createdUser: {},
        loginUser: {},
        sigup: false
      }
    },
    computed: {},
    methods: {
      login() {
        this.$store.dispatch('login', this.loginUser)
      },
      signUp() {
        this.$store.dispatch('signup', this.createdUser)
      }
    },
    components: {
      Navbar
    }
  }

</script>

<style>
  .login-page {
    height: 90vh;
    display: flex;
    justify-content: center;
    align-content: center;
    background-color: #eee;
  }

</style>
