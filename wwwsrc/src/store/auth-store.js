import vue from 'vue';
import router from '../router';

import axios from 'axios';

var auth = axios.create({
    baseURL: "//localhost:5000/auth/",
    withCredentials: true
});

export default {
    actions: {
        //region START AUTH ROUTES
      login({ commit, dispatch }, payload) {
        console.log("LOGIN PAYLOAD", payload)
        auth.post('login', payload)
        .then(res => {
            console.log("LOGIN RES", res)
            commit('setUser', res.data)
            router.push({ name: 'Home' })
          })
          .catch(err => {
            console.log('INVALID USERNAME OR PASSWORD')
          })
      },
      authenticate({ commit, dispatch }) {
        auth.get('authenticate')
          .then(res => {
            console.log('AUTHENTICATE', res.data)
            commit('setUser', res.data)
          })
          .catch(err => {
            console.log(err)
          })
      },
      signup({commit,dispatch}, payload) {
        auth.post('register', payload)
          .then(res => {
            commit('setUser', res.data)
            router.push({ name: 'Home' })
          })
          .catch(err => {
            console.log(err)
          })
      },
      logout({commit, dispatch}) {
        auth.delete('logout')
        .then(res => {
          commit('clearData')
        })
      }
      //endregion END AUTH ACTIONS
      
    }
};