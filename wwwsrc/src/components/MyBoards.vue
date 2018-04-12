<template>
  <div class="myboard">
    <navbar class="pad-b"></navbar>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="flexor space">
            <div class="pad-b">
              <h1>Your Boards</h1>
            </div>
            <div>
              <div data-toggle="modal" data-target="#boardModal" class="circle board-green pointer flexor">
                <i class="far fa-clipboard"></i>
              </div>
            </div>
          </div>
        </div>
        <div class="col-6" v-for="board in userBoards">
          <boards :board="board"></boards>
        </div>
      </div>
    </div>

    <!--BOARD MODAL BELOW-->
    <div class="modal fade" id="boardModal" tabindex="-1" role="dialog" aria-labelledby="modalForBoard" aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="modalForKeepLabel">Add a Board</h5>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="createBoard()">
                    <div class="form-group">
                      <h6>Name</h6>
                      <input type="text" class="form-control" v-model="createdBoard.name" placeholder="New Board Name" required>
                    </div>
                    <button type="submit" class="btn btn-sm btn-block btn-outline-primary">Add Board</button>
                  </form>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-sm btn-outline-secondary" data-dismiss="modal">Close</button>
                </div>
              </div>
            </div>
          </div>
  </div>
</template>

<script>
  import Boards from './Boards.vue'
  import Navbar from './Navbar.vue'
  export default {
    name: 'MyBoards',
    data() {
      return {
        createdBoard: {}
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      userBoards() {
        return this.$store.state.userBoards
      }

    },
    mounted() {
      this.$store.dispatch('getUserBoards', this.$route.params.userId)
    },
    methods: {
        createBoard() {
        this.$store.dispatch('createBoard', {board: this.createdBoard, userId: this.$route.params.userId})
        $('#boardModal').modal('hide');
        this.createdBoard = {};
      }
    },
    components: {
      Navbar,
      Boards
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .circle {
    margin: auto;
    height: 40px;
    width: 40px;
    border-radius: 50%;
    color: white;
    justify-content: center;
    align-items: center;
  }

  .board-green {
    color: white;
    border: .1rem solid #fff;
    background-color: #43AA8B;
    transition: all .3s linear
  }

  .board-green:hover {
    color: #43AA8B;
    background-color: #fff;
    border: .1rem solid #43AA8B
  }

.space {
    justify-content: space-between;
}
</style>
