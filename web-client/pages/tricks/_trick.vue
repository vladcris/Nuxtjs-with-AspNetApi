<template>
  <div class="d-flex justify-center align-start">

    <div v-if="submissions">
      <div v-for="x in 20">
        <div class="mx-2" v-for="s in submissions">
          {{s.id}} - {{s.description}} - {{s.trickId}}
          <div>
            <video width="500" controls :src="`http://localhost:5007/api/videos/${s.video}`" ></video>
          </div>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      Trick : {{ $route.params.trick}}
    </div>
  </div>
</template>

<script>
  import {mapState} from 'vuex';
  export default {
    computed : {
      ...mapState('submissions', ['submissions'])
    },
    async fetch(){
      const trickId = this.$route.params.trick;
      await this.$store.dispatch("submissions/fetchSubmissionsForTrick", {trickId: trickId}, {root: true})
    }
  }
</script>

<style scoped>
  .sticky {
    position: sticky;
    top: 48px;
  }
</style>
