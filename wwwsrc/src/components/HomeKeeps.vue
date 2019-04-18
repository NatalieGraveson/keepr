<template>
  <div class="homekeeps col-3 justify-content-center">
    <div class="card">
      {{keepsData.name}}
      <img v-bind:src="keepsData.img" />
      {{keepsData.description}}
      <!-- <i class="fas fa-trash" @click="removeKeep(keepsData._id)"></i> -->
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
    max-width: 30vh;
    max-height: 30vh;
  }
</style>