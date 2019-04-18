<template>
  <div class="vaults row">
    <div class="card col-4 vaultcard">
      {{vaultData.name}}





      <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#viewVault' + vaultData.id"
        @click="setActive">
        Launch demo modal
      </button>


      <!-- Modal -->
      <div class="modal fade" :id="'viewVault' + vaultData.id" tabindex="-1" role="dialog"
        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header ">
              <h5 class="modal-title col-12 text-center">{{vaultData.name}}</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <h1>{{vaultData.description}}</h1>
              <div v-for="keep in vaultKeeps" :keepData="keep">
                <!-- <div class="card"> -->
                <h1>{{keep.name}} <i class="fas fa-trash" @click="deleteKeepFromVault(keep.id)"></i></h1>
                <!-- </div> -->
              </div>
              <!-- added keeps will go here -->
            </div>
            <div class="modal-footer">
              <i class="fas fa-trash" @click="removeVault"></i>
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
          </div>
        </div>
      </div>

    </div>

  </div>
</template>

<script>
  export default {
    name: "vaults",
    props: ['vaultData'],
    mounted() {
      this.$store.dispatch('getVaultKeeps', this.vaultData.id)
    },
    data() {
      return {}
    },
    computed: {
      vaultKeeps() {
        return this.$store.state.vaultKeeps
      },
      activeVault() {
        this.$store.state.activeVault
      }
    },
    methods: {
      removeVault() {
        this.$store.dispatch("removeVault", this.vaultData.id);
      },
      setActive() {
        this.$store.dispatch("setActive", this.vaultData);
        this.$store.dispatch('getVaultKeeps', this.vaultData.id)
      },
      deleteKeepFromVault(keepId) {
        let payload = {
          vaultid: this.vaultData.id,
          keepId
        }
        this.$store.dispatch('deleteKeepFromVault', payload)
      }


    },

    components: {}
  }
</script>
<style scoped>
  .vaultcard {
    background-color: pink;
    height: 250px;
    width: 609px;
  }
</style>