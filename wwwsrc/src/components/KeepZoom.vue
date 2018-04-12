<template>
  <div class="keep-zoom">
    <navbar class="pad-b"></navbar>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <img class="img-fluid" :src="keepData.mediaUrl">
          <div class="flexor endr">
            <div class="pad-r">
              <p>
                <i class="fas dark-grey fa-binoculars"></i> {{keepData.keepViews}}</p>
            </div>
            <div class="pad-r">
              <p>
                <i class="fas dark-grey fa-save"></i> {{keepData.addedToBoard}}</p>
            </div>
            <div v-if="user.id">
              <div data-toggle="modal" :data-target="'#' + keepData.id" @click.prevent="getBoards()" class="circle2 pin-green2 pointer flexor">
                <i class="fas small-size fa-thumbtack"></i>
              </div>
            </div>
          </div>
        </div>
        <div class="col-12">
          <h1>{{keepData.name}}</h1>
          <p>{{keepData.description}}</p>
        </div>
      </div>
    </div>
    <!--ADD KEEP MODAL-->
    <div class="modal fade" :id="keepData.id" tabindex="-1" role="dialog" aria-labelledby="modalForKeep" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Add Keep to Board</h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="addToBoard(), addedToBoardCount()">
              <div class="form-group">
                <h6>Choose a Board</h6>
                <select class="form-control form-control-sm" v-model="b.board" required>
                  <option v-for="board in boards" :value="board.id">{{board.name}}</option>
                </select>
              </div>
              <button type="submit" class="btn btn-sm btn-block btn-outline-primary">Add To Board</button>
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
  import Navbar from './Navbar.vue'
  export default {
    name: 'KeepZoom',
    data() {
      return {
        b: {}
      }
    },
    mounted() {
      this.$store.dispatch('getAKeep', this.$route.params.keepId)
    },
    computed: {
      keepData() {
        return this.$store.state.singleKeep
      },
      user() {
        return this.$store.state.user
      },
      boards() {
        return this.$store.state.userBoards
      }
    },
    methods: {
      getBoards() {
        this.$store.dispatch('getUserBoards', this.user.id)
      },
      addedToBoardCount() {
        this.keepData.addedToBoard++;
        this.$store.dispatch('addBoardCount', this.keepData)
      },
      addToBoard() {
        this.$store.dispatch('addToBoard', {
          boardId: this.b.board,
          userId: this.user.id,
          keepId: this.keepData.id
        })
        $('#' + this.keepData.id).modal('hide')
      }
    },
    props: [],
    components: {
      Navbar
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .circle2 {
    margin: auto;
    height: 40px;
    width: 40px;
    border-radius: 50%;
    color: white;
    justify-content: center;
    align-items: center;
  }

  .pin-green2 {
    border: .1rem solid #fff;
    background-color: #43AA8B;
    transition: all .3s linear
  }

  .pin-green2:hover {
    color: #254441;
    background-color: #fff;
    border: .1rem solid #43AA8B
  }

  .endr {
      justify-content: flex-end;
      align-items: center
  }

  .endr p {
      margin-bottom: 0;
  }

</style>
