<template>
  <div class="keep">
    <router-link :to="{path: '/keep/' + keep.id}">
      <img @click="addView()" class="cover img-fluid" :src="keep.mediaUrl">
    </router-link>
    <div class="flexor centa pad-t">
      <div class="pad-r">
        <p>
          <i class="fas dark-grey fa-binoculars"></i> {{keep.keepViews}}</p>
      </div>
      <div class="pad-r">
        <p>
          <i class="fas dark-grey fa-save"></i> {{keep.addedToBoard}}</p>
      </div>
      <div v-if="user.id">
        <div data-toggle="modal" :data-target="'#' + keep.id" @click.prevent="getBoards()" class="circle2 pin-green2 pointer flexor">
          <i class="fas small-size fa-thumbtack"></i>
        </div>
      </div>
    </div>
    <!--MOVE KEEP MODAL-->
    <div class="modal fade" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="modalForKeep" aria-hidden="true">
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
  export default {
    name: 'Keep',
    data() {
      return {
        addedKeepToBoard: {},
        b: {}
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      boards() {
        return this.$store.state.userBoards
      }
    },
    methods: {
      addToBoard() {
        this.$store.dispatch('addToBoard', {
          boardId: this.b.board,
          userId: this.user.id,
          keepId: this.keep.id
        })
        $('#' + this.keep.id).modal('hide')
      },
      getBoards() {
        this.$store.dispatch('getUserBoards', this.user.id)
      },
      addView() {
        this.keep.keepViews++;
        this.$store.dispatch('addViewCount', this.keep)
      },
      addedToBoardCount() {
        this.keep.addedToBoard++;
        this.$store.dispatch('addBoardCount', this.keep)
      }
    },
    props: ['keep']
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .keep {
    border: .1rem solid #eaeaea;
    padding: .5rem;
    border-radius: 10px;
  }

  .centa {
    justify-content: center;
    align-items: center;
  }

  .centa p {
    margin-bottom: 0;
  }

  .grey {
    color: #cccccc;
    transition: all .3s linear;
  }

  .grey:hover {
    color: orangered;
  }

  .circle2 {
    margin: auto;
    height: 30px;
    width: 30px;
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

</style>
