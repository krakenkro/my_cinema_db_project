<template>
   <b-container>
       <h3 class="list-title">Фильмы</h3>
       <b-row>
           <template>
               <b-col cols="3" v-for="(movie, key) in movies" :key="key">
                   <MovieItem :movie="movie"
                   @showModal="onShowMovieInfo"
                   />
               </b-col>
           </template>
       </b-row>

       <b-modal :id="movieInfoModalID" size="xl" hide-footer hide-header>
           <p>text</p>
    
        </b-modal>
   </b-container>
</template>
<script>
import MovieItem from './MovieItem'
import axios from "axios";
const url = "https://reqres.in/api/users?page=2"
export default {
    name: 'MoviesList',
    data() {
    return {
        movies: [],
        movieInfoModalID: 'movie-info'
    };
  },
  components: {
    MovieItem

  },
  methods:{
      onShowMovieInfo(id){
          console.log(id);
          this.$bvmodal.show(this.movieInfoModalID)
      }
  },

  async mounted() {
    await axios.get(url).then((response) => {
      this.movies = response.data.data;
    });
  },
}
</script>
<style scoped>
.list-title{
    font-size: 50px;
    margin-bottom: 30px;
}
</style>