<template>
  <div>
   <h4>{{message}}</h4>
  </div>
</template>

<script>
export default {
  data: () => ({
    message: "" 
  }),
  async created() {
    this.message = await $fetch('https://localhost:7286/api/home')
  }
}
</script>