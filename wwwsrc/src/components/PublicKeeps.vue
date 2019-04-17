<template>
  <div class="col-3 justify-content-center publickeeps">
    <div class="card">
      {{keepsData.name}}
      <img v-bind:src="keepsData.img" />
      {{keepsData.description}}
    </div>
  </div>

  </div>

  </div>
</template>

<script>
  export default {
    name: "publickeeps",
    props: ['keepsData'],
    data() {
      return {}
    },
    computed: {
      allKeeps() {
        return this.$store.state.keeps
      }
    },
    methods: {},
    components: {}
  }
</script>
<style scoped>
  img {
    max-width: 30vh;
    max-height: 30vh;
  }
</style>