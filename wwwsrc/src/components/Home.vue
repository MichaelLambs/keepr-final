<template>
  <div class="home">
    <navbar></navbar>
    <div class="container-fluid">
      <div v-if="user.id" class="row">
        <div class="col-11">
          <div class="row">
            <div v-for="keep in allKeeps" class="col-3">
              <div class="marg-bot">
                <keep :keep="keep"></keep>
              </div>
            </div>
          </div>
        </div>
        <div class="col-1 sticky-top stick dark">
          <div class="flexor pad-b column-direction">
            <div>
              <h1>K</h1>
            </div>
            <div>
              <h1>E</h1>
            </div>
            <div>
              <h1>E</h1>
            </div>
            <div>
              <h1>P</h1>
            </div>
            <div class="pad-b">
              <h1>R</h1>
            </div>
            <div class="pad-b2">
              <div data-toggle="modal" data-target="#keepModal" class="circle pin-green pointer flexor">
                <i class="fas fa-thumbtack"></i>
              </div>
            </div>
            <div>
              <div data-toggle="modal" data-target="#boardModal" class="circle board-green pointer flexor">
                <i class="far fa-clipboard"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="row">
        <div v-for="keep in allKeeps" class="col-3">
          <div class="marg-bot">
            <keep :keep="keep"></keep>
          </div>
        </div>
      </div>
    </div>
    <!--KEEP MODAL BELOW-->
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="modalForKeep" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="modalForKeepLabel">Add a Keep</h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createKeep()">
              <div class="form-group">
                <label>Name</label>
                <input type="text" class="form-control" v-model="createdKeep.name" placeholder="Name" required>
              </div>
              <div class="form-group">
                <label>Image URL</label>
                <input type="url" class="form-control" v-model="createdKeep.mediaUrl" placeholder="Image URL" required>
              </div>
              <div class="form-group">
                <label>Description</label>
                <input type="text" class="form-control" v-model="createdKeep.description" placeholder="Description">
              </div>
              <button type="submit" class="btn btn-sm btn-block btn-outline-primary">Add Keep</button>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-sm btn-outline-secondary" data-dismiss="modal">Close</button>
          </div>
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
  import Keep from './Keep.vue';
  import Navbar from './Navbar.vue'
  export default {
    name: 'Home',
    data() {
      return {
        createdKeep: {},
        createdBoard: {}
      }
    },
    computed: {
      allKeeps() {
        return this.$store.state.allKeeps;
      },
      user() {
        return this.$store.state.user
      }
    },
    methods: {
      createKeep() {
        this.$store.dispatch('createKeep', {keep: this.createdKeep, userId: this.user.id})
        $('#keepModal').modal('hide');
        this.createdKeep = {};
      },
      createBoard() {
        this.$store.dispatch('createBoard', {board: this.createdBoard, userId: this.user.id})
        $('#boardModal').modal('hide');
        this.createdBoard = {};
      },
    },
    mounted() {
      this.$store.dispatch('getAllKeeps');
    },
    components: {
      Navbar,
      Keep
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .dark {
    height: 90vh;
  }

  .marg-bot {
    margin-bottom: 1rem;
  }

  .column-direction {
    align-items: center;
    height: 90vh;
    justify-content: flex-end;
    flex-direction: column;
  }

  .column-direction h1 {
    margin-bottom: 0;
    font-family: 'Monoton', cursive;
    font-size: 3rem;
    color: #FF6F59;
  }

  .pin-green {
    border: .1rem solid #fff;
    background-color: #43AA8B;
    transition: all .3s linear
  }

  .pin-green:hover {
    color: #254441;
    background-color: #fff;
    border: .1rem solid #43AA8B
  }

  .board-green {
    border: .1rem solid #fff;
    background-color: #254441;
    transition: all .3s linear
  }

  .board-green:hover {
    color: #43AA8B;
    background-color: #fff;
    border: .1rem solid #43AA8B
  }

  .stick {
    top: 10vh;
  }

  .circle {
    margin: auto;
    height: 50px;
    width: 50px;
    border-radius: 50%;
    color: white;
    justify-content: center;
    align-items: center;
  }

</style>
