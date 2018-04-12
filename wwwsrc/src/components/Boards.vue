<template>
  <div class="boards">
    <h2>{{board.name}}</h2>
    <!-- <i @click="deleteBoard(board)"class="fas fa-trash"></i> -->
    <div class="list-group" v-for="bkeep in boardKeeps">
      <keeponboard class="list-group-item" :board="board" :bkeep="bkeep"></keeponboard>
    </div>
  </div>
</template>

<script>
  import KeepOnBoard from './KeepOnBoard.vue'
  export default {
    name: 'Boards',
    data() {
      return {}
    },
    mounted(){
      return this.$store.dispatch('getKeepsByBoard', this.board.id)
    },
    computed: {
      boardKeeps(){
        return this.$store.state.boardKeeps[this.board.id] || []
      }
    },
    methods: {
      // deleteBoard(board) {
      //   this.$store.dispatch('deleteBoard', {board: this.board, userId})
      // }
    },
    props: ["board"],
    components: {
      keeponboard: KeepOnBoard
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>


</style>
