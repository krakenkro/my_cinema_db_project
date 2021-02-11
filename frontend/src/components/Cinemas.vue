<template>
    <b-container>
        <b-row> 
            <div v-for="(cinema, key) in cinemas" :key="key">
                {{cinema.name}}
            </div>
        </b-row>
    </b-container>
</template>
<style scoped>

</style>
<script>
import axios from "axios";
const url = "https://localhost:44319/odata/Cinemas";
export default {
    name:"Cinemas",
    data() {
        return {
            cinemas: [],
        };
    },
    async mounted() {
    await axios.get(url).then((response) => {
      this.cinemas = response.data.value;
    });
  },
}
</script>