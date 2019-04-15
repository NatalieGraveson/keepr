<template>
  <div class="publickeeps container-fluid">
    <div class="row">
      <div class="col-12">
        {{keepsData.name}}
        <img v-bind:src="keepsData.img" />
        {{keepsData.description}}
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