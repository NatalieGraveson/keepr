<template>
  <div class="home">
    <nav class="navbar navbar-dark bg-dark">
      <span class="navbar-brand mb-0 h1">Navbar</span>
      <!-- <i @click="router.push({ name: 'Profile' })" class="far fa-user text-white"></i> -->
      <router-link class="ml-auto far fa-user text-white" :to="{name: 'Profile'}">
      </router-link>

      <button type="button" @click="logOut">Logout</button>
    </nav>
    <h1>Welcome Home</h1>
    <home-keeps v-for="keep in keeps" :keepsData='keep'></home-keeps>
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