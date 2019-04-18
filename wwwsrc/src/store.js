import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let auth = Axios.create({
  baseURL: baseUrl + "account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    savedKeeps: [],
    vaults: [],
    vaultKeeps: [],
    ActiveVault: {},
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setSavedKeeps(state, payload) {
      state.savedKeeps.push(payload)
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setVaultKeeps(state, payload) {
      state.vaultKeeps = payload
    },
    setActiveVault(state, payload) {
      state.ActiveVault = payload
    },
    clearVaultKeep(state) {

      state.vaultKeeps = []
    }
  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {

      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logOut({ commit, dispatch }) {
      auth.delete('logOut')
        .then(res => {
          router.push({ name: 'login' })
        })
    },
    //keeps on login page 
    getPubKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
    },
    //home keeps
    SaveKeep({ commit, dispatch }, payload) {
      commit('setSavedKeeps', payload)
    },
    removeKeep({ commit, dispatch }, payload) {
      api.delete('keeps/' + payload)
        .then(res => {
          dispatch('getKeeps')
        })
    },
    //profile keeps
    getKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => {
          commit('setKeeps', res.data)

        })
    },
    addKeep({ commit, dispatch }, keepData) {
      api.post('keeps', keepData)
        .then(res => {
          dispatch('getKeeps', 'getPubKeeps')
        })

    },
    //profile vaults
    getVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)

        })

    },
    addVault({ commit, dispatch }, payload) {
      api.post('vaults', payload)
        .then(res => {
          dispatch('getVaults')
        })
    },
    removeVault({ commit, dispatch }, payload) {
      api.delete('vaults/' + payload)
        .then(res => {
          dispatch('getVaults')

        })
    },
    //vaultkeeps
    getVaultKeeps({ commit, dispatch }, payload) {
      api.get('vaultkeeps/' + payload)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
    },
    createVaultKeep({ commit, dispatch }, payload) {
      api.post('vaultkeeps/', payload)
        .then(res => {
          dispatch('getVaultKeeps', payload.vaultId)
        })
    },
    setActive({ commit, dispatch }, payload) {
      commit('setActiveVault', payload)

    },
    clearVaultKeeps({ commit }) {
      commit('clearVaultKeep')
    },
    deleteKeepFromVault({ dispatch }, payload) {
      debugger
      api.delete('vaultkeeps/' + payload.vaultid + '/' + payload.keepId)
        .then(res => {
          dispatch('getVaultKeeps', payload.vaultid)
        })
    }
  }
})