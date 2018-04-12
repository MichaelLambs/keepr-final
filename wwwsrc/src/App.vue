<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App',
  mounted() {
      this.$store.dispatch('authenticate')
    }
}
</script>

<style>
  .flexor{
        display: flex;
    }
    .pad-r{
        padding-right: 1rem;
    }
    .pad-t {
      padding-top: 1rem;
    }
    .pointer {
      cursor: pointer;
    }
    .cover {
    object-fit: cover;
  }
  .small-size {
    font-size: .8rem;
  }
  .pad-b {
    padding-bottom: 1rem;
  }
  .pad-b2 {
    padding-bottom: .5rem;
  }
  .marg-b {
    margin-bottom: 1rem;
  }
  .dark-grey {
    color: rgb(122, 122, 122);
  }
</style>
