<template>
  <div class="col-3 justify-content-center publickeeps">
    <div class="card mt-5">
      <h2>{{keepsData.name}}</h2>
      <img v-bind:src="keepsData.img" />
      <h4>{{keepsData.description}}</h4>
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
    max-width: 47vh;
    max-height: 53vh;
  }

  .card {
    height: min-content;
    border: 3px solid #969da5;
  }

  h2 {
    color: black;
  }

  h4 {
    color: #ed6761;
  }
</style>