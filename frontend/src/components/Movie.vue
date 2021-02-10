<template>
<b-container>
  <h1 class="movie_title">Фильмы</h1>
  <b-row>
    <template>
      <div >
        <b-col :v-for="movie in movies" cols="8" style="max-width: 650px;">
          <b-card no-body border-variant="success" class="overflow-hidden mb-3">
            <b-row no-gutters>
              <b-col md="4" class="poster">
                <b-card-img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQI-SaYWlXmVicHWYEEpRgrmFir507tWQk3pA&usqp=CAU" alt="Image" class="rounded-0"></b-card-img>
              </b-col>
              <b-col md="8">
                <b-card-body title="Horizontal Card">
                  <b-card-text>
                    Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sint commodi eos eveniet possimus, veritatis officiis eaque tenetur at dolorem architecto neque. Nobis, impedit magnam maxime doloremque voluptatem rerum nisi numquam.
                  </b-card-text>
                </b-card-body>
            </b-col>
            </b-row>
          </b-card>
        </b-col>
      </div>
      <b-col cols="4">
          
      </b-col>

      
    </template>
  </b-row>
  <div>
      <div v-for="movie in movies" :key="movie.id">
        <h2>{{movie.first_name}}</h2>
      </div>
  </div>
</b-container>
    
</template>
<script>
import axios from "axios";
//const url = "https://localhost:44319/odata";
const url = "https://reqres.in/api/users?page=2"
export default {
  data() {
    return {
        movies: [],
    };
  },

  async mounted() {
    await axios.get(url).then((response) => {
      this.movies = response.data.data;
    });
  },
}
</script>
<style scoped>
.movie_title{
  text-align: start;
  font-size: 50px;
  margin-bottom: 30px;
}
.poster{
      display: flex;
    justify-content: center;
    align-items: center;
    
}
.poster img{
  width: 100%;
  height: auto;
  padding-left: 1.25rem;
}
.border-success {
    border-color: #563d7c !important;
}
</style>