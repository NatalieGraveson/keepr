<template>
  <div class="vaults row">
    <div class="card col-4 vaultcard">
      {{vaultData.name}}

    </div>

  </div>
</template>

<script>
  export default {
    name: "vaults",
    props: ['vaultData'],
    data() {
      return {}
    },
    computed: {},
    methods: {},
    components: {}
  }
</script>
<style scoped>
  .vaultcard {
    background-color: pink;
    height: 250px;
  }
</style>