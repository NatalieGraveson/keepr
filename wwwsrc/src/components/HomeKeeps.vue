<template>
  <div class="homekeeps col-3 justify-content-center">
    <div class="card mt-3" style="width: 18rem;">
      <img v-bind:src="keepsData.img" class="card-img-top">
      <div class="card-body">
        <h5 class="card-title">{{keepsData.name}}</h5>
        <p class="card-text">{{keepsData.description}}
        </p>

      </div>
    </div>

  </div>

  </div>

  </div>
</template>

<script>
  export default {
    name: "homekeeps",
    props: ['keepsData'],
    data() {
      return {}
    },
    computed: {
      allKeeps() {
        return this.$store.state.keeps
      }
    },
    methods: {

    },
    components: {}
  }
</script>

<style scoped>
  img {
    max-width: 47vh;
    max-height: 53vh;
  }
</style>