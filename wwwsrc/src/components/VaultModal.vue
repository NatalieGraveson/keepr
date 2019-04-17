<template>
  <div class="VaultModal">


    <!-- Modal -->
    <div class="modal fade" id="addVault" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title"></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">

            <form @submit.prevent="addVault">
              <input class="bg-light form-control" type="text" placeholder="title" v-model="newVault.name" required>
              <input class="bg-light form-control border border-info text-dark" type="text" placeholder="description"
                v-model="newVault.description">
              <div class="d-flex justify-content-center">
                <button class="mt-3 form-control border" type="submit">Create</button>
              </div>
            </form>

          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "",
    props: ['vaultData'],
    data() {
      return {
        newVault: {
          name: "",
          description: "",
        }
      }
    },
    computed: {},
    methods: {
      addVault() {
        this.$store.dispatch("addVault", this.newVault)
        this.newKeep = { name: "", description: "" };
      },
    },
    components: {}
  }
</script>