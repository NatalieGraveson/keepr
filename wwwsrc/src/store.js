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
    vaults: []
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

    }

  }
})
