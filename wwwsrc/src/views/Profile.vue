<template>
  <div class="profile">
    <nav class="navbar navbar-dark">
      <span class="navbar-brand mb-0 h1">Keepr</span>
      <!-- <i @click="router.push({ name: 'Profile' })" class="far fa-user text-white"></i> -->
      <router-link class="ml-auto mr-3 fas fa-home text-white fa-2x" :to="{name: 'home'}">
      </router-link>
      <button type="button" class="btn btn-outline-light" @click="logOut">Logout</button>
    </nav>
    <div class="container-fluid">
      <i class="fas fa-plus mr-auto" data-toggle="modal" data-target="#addKeep"></i>
      <div class="row">
        <div class="col-12">

          <div class="modal fade" id="addKeep" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
            aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title">Modal title</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">

                  <form @submit.prevent="addKeep">
                    <input class="bg-light form-control border border-info text-dark" type="text" placeholder="title"
                      v-model="newKeep.name" required>
                    <input class="bg-light form-control border border-info text-dark" type="text"
                      placeholder="description" v-model="newKeep.description">
                    <input class="bg-light form-control border border-info text-dark" type="text" placeholder="image"
                      v-model="newKeep.img">
                    <input type="radio" v-model="newKeep.isprivate" :value="true">
                    <input type="radio" v-model="newKeep.isprivate" :value="false">

                    <!-- {{newKeep.isprivate}} -->
                    <h1 v-if="newKeep.isprivate == false">Public</h1>
                    <h1 v-else>Private</h1>
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
          <div class="row">
            <div class="col-3" v-for="keep in keeps">
              <div class="card">
                <h1>{{keep.name}}</h1>
                <img v-bind:src="keep.img" />
                <i class="fas fa-trash" @click="removeKeep(keep.id)"></i>
                <button class="btn btn-outline-dark" data-toggle="modal" data-target="#openModal">view</button>
                <div class="dropdown">
                  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Keep
                  </button>
                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a v-for="vault in vaults" class="dropdown-item"
                      @click="createVaultKeep(vault, keep)">{{vault.name}}</a>

                  </div>
                </div>
                </form>

              </div>

              <keep-modal v-for="keep in keeps" :keepData='keep'></keep-modal>

            </div>
          </div>
        </div>
      </div>
      <hr>
      <i class="fas fa-plus mr-auto" data-toggle="modal" data-target="#addVault"></i>
      <div class="row">
        <div class="col-12 mt-5 vaultSpot">
          <vault-modal v-for="vault in vaults" :vaultData='vault'></vault-modal>
          <vaults v-for="vault in vaults" :vaultData='vault'></vaults>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import KeepModal from '@/components/KeepModal.vue'
  import Vaults from '@/components/Vaults.vue'
  import VaultModal from '@/components/VaultModal.vue'
  export default {
    name: "profile",
    props: [],
    mounted() {
      this.$store.dispatch('getKeeps');
      this.$store.dispatch('getVaults');

      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    data() {
      return {
        showAddVaultKeep: false,
        newKeep: {
          name: "",
          description: "",
          img: "",
          isprivate: false,
        }
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      keeps() {
        return this.$store.state.keeps
      },
      vaults() {
        return this.$store.state.vaults
      },
      vaultKeeps() {
        return this.$store.state.vaultKeeps
      }

    },
    methods: {
      addKeep() {
        this.$store.dispatch("addKeep", this.newKeep)
        this.newKeep = { name: "", description: "", img: "" };
      },
      createVaultKeep(vault, keep) {
        let payload = {
          vaultid: vault.id,
          keepid: keep.id,
          userid: this.user.id
        }
        this.$store.dispatch('createVaultKeep', payload)
      },
      logOut() {
        this.$store.dispatch('logOut')
      },
      removeKeep(keepId) {
        this.$store.dispatch("removeKeep", keepId);
      },
    },
    components: {
      Vaults,
      VaultModal,
      KeepModal,
    }
  }
</script>

<style scoped>
  .navbar {
    background-color: #ed6761;
  }

  img {
    max-width: 100%;
    max-height: 64vh;
    margin-left: 37px;
    margin-right: 37px;
  }

  .vaultSpot {
    display: flex;
    text-align: center;
    justify-content: center;
    flex-direction: row;

  }
</style>