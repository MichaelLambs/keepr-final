<template>
  <div>
    <nav class="navbar">
      <div class="flexor">
        <router-link :to="{name: 'Home'}">
          <div class="title">
            <h1>KEEPR</h1>
          </div>
        </router-link>
      </div>
      <div v-if="!user.id">
        <div class="dropdown pointer">
          <a class="dropdown-toggle" role="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span>
              <i class="fas fa-sign-in-alt"></i>
            </span>
          </a>
          <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
            <div class="dropdown-item pointer">
              <router-link to="/">Keeps</router-link>
            </div>
            <div class="dropdown-item pointer">
              <router-link to="/login">Sign Up</router-link>
            </div>
            <div class="dropdown-item pointer">
              <router-link to="/login">Login</router-link>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="dropdown pointer">
        <a class="dropdown-toggle" role="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          <span>{{user.name}}
            <i class="fas fa-user"></i>
          </span>
        </a>
        <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
          <div class="dropdown-item pointer">
            <router-link :to="{path: '/mykeeps/' + user.id}">Your Keeps</router-link>
          </div>
          <div class="dropdown-item pointer">
            <router-link :to="{path: '/myboards/' + user.id}">Saved Boards</router-link>
          </div>
          <div @click.prevent="logout" class="dropdown-item pointer">
            <p>Logout</p>
          </div>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
  export default {
    name: 'Navbar',
    data() {
      return {

      }
    },
    computed: {
      user() {
        return this.$store.state.user
      }
    },
    methods: {
      logout() {
        this.$store.dispatch('logout')
      }
    }
  }

</script>


<style scoped>
  .title h1 {
    color: white;
    font-family: 'Monoton', cursive;
    margin-bottom: 0;
    transition: all .3s linear;
  }

  .title h1:hover {
    font-size: 2.75rem;
  }

  .title {
    border-radius: 10px;
    box-shadow: 0 0 10px rgb(201, 92, 75);
    padding: .5rem;
  }

  .full-width {
    width: 100%;
  }

  .mid {
    margin: .5rem auto;
  }

  input {
    width: 95%;
  }

  a {
    color: #333;
  }

  a:hover {
    text-decoration: none
  }

  .pointer {
    cursor: pointer;
  }

  .navbar {
    background-color: #FF6F59;
    /* background-color: #7BA6D0; */
    color: white;
    height: 10vh;
  }

  .navbar h3 {
    font-weight: 800;
    font-size: 2rem;
  }

  .dropdown-menu {
    min-width: 0;
    padding: 1rem 0;
    margin-right: 10px;
    left: -8rem;
    z-index: 1021;
  }

  .dropdown-item:hover {
    background-color: #f9f9f9;
  }

  .flexor {
    display: flex;
    align-items: flex-end;
    justify-content: space-around;
  }

  .flexor h3 {
    margin-bottom: 0;
  }

  .pad-r {
    padding-right: 1rem;
  }

</style>
