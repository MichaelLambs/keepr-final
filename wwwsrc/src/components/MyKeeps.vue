<template>
  <div class="myboard">
    <navbar class="pad-b"></navbar>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="flexor space">
            <div>
              <h1>Your Keeps</h1>
            </div>
            <div>
              <div data-toggle="modal" data-target="#keepModal" class="circle board-green pointer flexor">
                <i class="fas fa-thumbtack"></i>
              </div>
            </div>
          </div>
        </div>
        <div class="col-4" v-for="ukeep in userKeeps">
          <userkeep :ukeep="ukeep"></userkeep>
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
  </div>
</template>

<script>
  import UserKeep from './UserKeep.vue'
  import Navbar from './Navbar.vue'
  export default {
    name: 'MyKeeps',
    data() {
      return {
        createdKeep: {}
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      userKeeps() {
        return this.$store.state.userKeeps
      }
    },
    mounted() {
      this.$store.dispatch('getUserKeeps', this.$route.params.userId)
    },
    methods: {
      createKeep() {
        this.$store.dispatch('createKeep', {keep: this.createdKeep, userId: this.user.id})
        $('#keepModal').modal('hide');
        this.createdKeep = {};
      }
    },
    components: {
      Navbar,
      userkeep: UserKeep
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
