<template>
  <div class="bkeep">
    <div class="flexor space">
      <div>
        <div class="flexor">
          <div class="pad-r">
            <router-link :to="{path: '/keep/' + bkeep.id}">
              <img class="cover img-fluid" :src="bkeep.mediaUrl" width="100" height="100">
            </router-link>
          </div>
          <div>
            <h4>{{bkeep.name}}</h4>
          </div>
        </div>
      </div>
      <div>
        <i data-toggle="modal" :data-target="'#' + bkeep.id" class="fas small-size pointer fa-thumbtack"></i>
      </div>
    </div>
    <!--KEEP MODAL BELOW-->
    <div class="modal fade" :id="bkeep.id" tabindex="-1" role="dialog" aria-labelledby="modalForKeep" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="modalForKeepLabel">Edit Pin: {{bkeep.name}}</h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="addToBoard()">
              <div class="form-group">
                <h6>Move to Board:</h6>
                <select class="form-control form-control-sm" v-model="b.board">
                  <option :placeholder="board.name" disabled>{{board.name}}</option>
                  <option v-for="daBoard in daBoards" :value="daBoard.id">{{daBoard.name}}</option>
                </select>
              </div>
              <button type="submit" @click="removeKeepFromBoard(bkeep.id)" class="btn btn-sm btn-outline-primary btn-block">Add To Board</button>
            </form>
          </div>
          <div class="modal-footer">
            <button @click="removeKeepFromBoard(bkeep.id), removeAddedCount()" type="button" class="btn btn-sm btn-outline-danger" data-dismiss="modal">Delete Pin</button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'KeepOnBoard',
    data() {
      return {
          b: {}
      }
    },
    computed: {
      daBoards() {
        return this.$store.state.userBoards
      },
      user() {
        return this.$store.state.user
      }
    },
    methods: {
      removeKeepFromBoard(keepId) {
        this.$store.dispatch('removeFromBoard', {
          keepId: keepId,
          boardId: this.board.id
        })
        $('#' + this.bkeep.id).modal('hide');
      },
      removeAddedCount() {
        this.bkeep.addedToBoard--
          this.$store.dispatch('removeAddedCount', this.bkeep)
      },
      addToBoard() {
        this.$store.dispatch('addToBoard', {
          boardId: this.b.board,
          userId: this.user.id,
          keepId: this.bkeep.id
        })
        $('#' + this.bkeep.id).modal('hide')
      }
    },
    props: ["bkeep", "board"]
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .space {
    justify-content: space-between;
  }

  .list-group-item {
    z-index: auto;
  }

  .endSpace {
    justify-self: flex-end;
  }

  .fa-thumbtack {
      color: #cccccc;
      transition: all .3s linear;
  }
  .fa-thumbtack:hover {
      color: #8b8b8b
  }

</style>
