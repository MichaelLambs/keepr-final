import vue from 'vue';
import vuex from 'vuex';

import axios from 'axios';
import router from '../router/index';
import authStore from './auth-store';

var serverAPI = axios.create({
  baseURL: '//localhost:5000/api',
  withCredentials: true
});

vue.use(vuex);

export default new vuex.Store({
  state: {
    user: {},
    allKeeps: [],
    userBoards: [],
    userKeeps: [],
    boardKeeps: {},
    singleKeep: {}
  },
  modules: {
    authStore
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload;
    },
    clearData(state, payload) {
      state.user = {};
      state.userBoards = [];
      state.userKeeps = [];
    },
    setAllKeeps(state, payload) {
      state.allKeeps = payload;
    },
    setUserKeeps(state, payload) {
      state.userKeeps = payload;
    },
    setUserBoards(state, payload) {
      state.userBoards = payload;
    },
    setBoardKeeps(state, payload){
      vue.set(state.boardKeeps, payload.boardId, payload.keeps);
    },
    setAKeep(state, payload) {
      state.singleKeep = payload;
    }
  },
  actions: {
    //GET ALL KEEPS FOR HOMEPAGE
    getAllKeeps({commit, dispatch}, payload) {
      serverAPI.get('keeps')
        .then(res => {
          console.log('GET ALL KEEPS', res.data)
          commit('setAllKeeps', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    //GET KEEPS BY USER
    getUserKeeps({commit, dispatch}, payload) {
      serverAPI.get('keeps/user/' + payload)
        .then(res => {
          commit('setUserKeeps', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    // GET SINGLE KEEP FOR KEEP PAGE
    getAKeep({commit, dispatch}, payload) {
      serverAPI.get('keeps/' + payload)
        .then(res => {
          commit('setAKeep', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    // ADD VIEW TO KEEP
    addViewCount({commit, dispatch}, payload) {
      serverAPI.put('keeps/' + payload.id, payload)
        .then(res => {
          dispatch('getAKeep', payload.id)
        })
        .catch(err => {
          console.log(err)
        })
    },
    makePublic({commit, dispatch}, payload) {
      serverAPI.put('keeps/' + payload.keep.id, payload.keep)
        .then(res => {
          dispatch('getAllKeeps')
          dispatch('getUserKeeps', payload.userId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    // ADD BOARD COUNT TO KEEP
    addBoardCount({commit, dispatch}, payload) {
      serverAPI.put('keeps/' + payload.id, payload)
        .then(res => {
          dispatch('getAllKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    },
    // CREATE A KEEP
    createKeep({commit, dispatch}, payload) {
      serverAPI.post('keeps', payload.keep)
        .then(res => {
          dispatch('getUserKeeps', payload.userId)
          dispatch('getAllKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    },
    // DELETE A KEEP
    deleteKeep({commit, dispatch}, payload) {
      serverAPI.delete('keeps/' + payload.keepId)
        .then(res => {
          dispatch('getUserKeeps', payload.userId)
          dispatch('getAllKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    },
    // GET ALL BOARDS BY USER
    getUserBoards({commit, dispatch}, payload) {
      serverAPI.get('boards/' + payload)
        .then(res => {
          console.log("USER BOARDS", res.data)
          commit('setUserBoards', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    // CREATE A BOARD
    createBoard({commit, dispatch}, payload) {
      serverAPI.post('boards', payload.board)
        .then(res => {
          dispatch('getUserBoards', payload.userId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    addToBoard({commit, dispatch}, payload) {
      console.log("MOVING KEEP PAYLOAD", payload)
      serverAPI.post('keeps/' + payload.boardId, payload)
        .then(res => {
          dispatch('getKeepsByBoard', payload.boardId)
          console.log(res.data)
        })
        .catch(err=> {
          console.log(err)
        })
    },
    getKeepsByBoard({commit, dispatch}, payload) {
      serverAPI.get('boards/' + payload + '/keeps', payload)
        .then(res => {
          console.log('KEEPS ON BOARD DATA', res.data)
          commit('setBoardKeeps', {boardId: payload, keeps: res.data})
        })
        .catch(err => {
          console.log(err)
        })
    },
    removeFromBoard({commit, dispatch}, payload) {      
      serverAPI.delete('boards/' + payload.boardId + '/keeps/' + payload.keepId)
        .then(res => {
          dispatch('getKeepsByBoard', payload.boardId)
        })
        .catch( err=> {
          console.log(err)
        })
    },
    removeAddedCount({commit,dispatch}, payload) {
      serverAPI.put('keeps/' + payload.id, payload)
        .then(res => {
          dispatch('getAllKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    }
  }
});
