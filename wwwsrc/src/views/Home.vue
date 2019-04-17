<template>
  <div class="home">
    <nav class="navbar navbar-dark">
      <span class="navbar-brand mb-0 h1">Keepr</span>
      <!-- <i @click="router.push({ name: 'Profile' })" class="far fa-user text-white"></i> -->
      <router-link class="ml-auto mr-3 far fa-user text-white fa-2x" :to="{name: 'profile'}">
      </router-link>

      <button type="button" class="btn btn-outline-light" @click="logOut">Logout</button>
    </nav>
    <h1>Welcome Home</h1>
    <div class="container-fluid">
      <div class="row">
        <home-keeps v-for="keep in keeps" :keepsData='keep'></home-keeps>
      </div>
    </div>
  </div>
</template>

<script>
  import HomeKeeps from '@/components/HomeKeeps.vue'
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch('getPubKeeps');
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      },

      user() {
        return this.$store.state.user
      }
    },

    methods: {
      logOut() {
        this.$store.dispatch('logOut')
      }
    },
    components: {
      HomeKeeps
    }

  };
</script>

<style scoped>
  .navbar {
    background-color: #ed6761;
  }
</style>