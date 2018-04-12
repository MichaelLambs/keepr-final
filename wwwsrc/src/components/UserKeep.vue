<template>
  <div class="user-keep">
    <img class="cover img-fluid" :src="ukeep.mediaUrl">
    <div class="flexor centa pad-t">
      <div class="pad-r">
        <p>
          <i class="fas dark-grey fa-binoculars"></i> {{ukeep.keepViews}}</p>
      </div>
      <div class="pad-r">
        <p>
          <i class="fas dark-grey fa-save"></i> {{ukeep.addedToBoard}}</p>
      </div>
      <div v-if="ukeep.keepPublic == 0">
        <div data-toggle="modal" :data-target="'#' + ukeep.id" class="circle2 pin-green2 pointer flexor">
          <i class="fas small-size fa-lock"></i>
        </div>
      </div>
      <div v-if="ukeep.keepPublic == 1">
            <div class="circle2 pin-grey flexor">
              <i class="fas small-size fa-lock-open"></i>
            </div>
          </div>
    </div>
    <!--MOVE UKEEP MODAL-->
    <div class="modal modal-uno fade" :id="ukeep.id" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Edit Keep</h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="editUKeep()">
              <div class="form-group">
                <h6>Name</h6>
                <input type="text" class="form-control" v-model="ukeep.name">
              </div>
              <div class="form-group">
                <h6>Image URL</h6>
                <input type="url" class="form-control" v-model="ukeep.mediaUrl">
              </div>
              <div class="form-group">
                <h6>Description</h6>
                <input type="text" class="form-control" v-model="ukeep.description">
              </div>
              <button type="submit" class="btn btn-sm btn-block btn-outline-primary">Edit Keep</button>
            </form>
          </div>
          <div class="modal-footer">
            <button data-toggle="modal" :data-target="'#public-' + ukeep.id" class="btn btn-sm btn-outline-success">Make Public</button>
            <div v-if="ukeep.userId == user.id && ukeep.keepPublic != 1">
              <button type="button" @click="deleteKeep(ukeep.id)" class="btn btn-sm btn-outline-danger" data-dismiss="modal">Delete</button>
            </div>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
    <!--PUBLIC UKEEP MODAL-->
    <div class="modal modal-dos fade" :id="'public-' + ukeep.id" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h2 class="modal-title">Make Keep Public?</h2>
          </div>
          <div class="modal-body">
            <div class="form-group">
              <h4>Making a Keep public will allow all users to see it.</h4>
              <p>You will not be able to
                <b>edit</b> or
                <b>delete</b> once a keep is public.</p>
              <h5>Is the world ready for your Genius?</h5>
            </div>
            <button type="submit" @click="makePublic()" data-toggle="modal" :data-target="'#public-' + ukeep.id" class="btn btn-block btn-sm btn-outline-success">YES</button>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-block btn-sm btn-outline-secondary" data-dismiss="modal">NOT YET</button>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>
  export default {
    name: 'UserKeep',
    data() {
      return {
        editedUKeep: {}
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      }
    },
    methods: {
      deleteKeep(keepId) {
        this.$store.dispatch('deleteKeep', {
          keepId: keepId,
          userId: this.user.id
        })
      },
      editUKeep() {
          this.$store.dispatch('makePublic', {keep: this.ukeep, userId: this.user.id})
        $('#' + this.ukeep.id).modal('hide');
      },
      makePublic() {
        this.ukeep.keepPublic = 1;
        this.$store.dispatch("makePublic", {
          keep: this.ukeep,
          userId: this.user.id
        })
        $('#' + this.ukeep.id).modal('hide');
      }
    },
    props: ['ukeep']
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

  .modal-dos {
    z-index: 1050;
  }

  .modal-uno {
      z-index: 1049;
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

  .pin-grey {
      color: #c4c4c4;
      background-color: #eaeaea;
  }

</style>
