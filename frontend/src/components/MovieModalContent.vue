<template>
    <div class="movie-info-wrap">
        <header class="movie-info-header">
            <h6 class="movie-header-title">Подробнее</h6>
            <BIconX class="close-icon" @click="closeModal"/>
        </header>
        <div class="movie-info-content">
            <b-row>
                <b-col>
                    <b-embed
                      type="iframe"
                      aspect="16by9"
                      :src="movie.trailer"
                      allowfullscreen
                    ></b-embed>
                </b-col>
            </b-row>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MovieModalContent',
    data: () => ({
    defaultPosterBg:
      "linear-gradient(45deg,rgb(0, 3, 38) 0%,rgb(82, 15, 117) 100%)"
  }),
    props:{
        movie:{
            type: Object,
            required: true
        }
    },
    computed:{
        posterStyle() {
      return {
        "background-image": this.posterBg,
        
      };
    },
    posterBg() {
      return this.movie ? `url(${this.movie.img})` : this.defaultPosterBg;
    }
    },
    methods:{
        closeModal(){
            this.$emit("closeModal");
        }
    }
}
</script>
<style scoped>
.movie-info-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem;
  background: linear-gradient(45deg, rgb(0, 3, 38) 0%, rgb(82, 15, 117) 100%);
  color: #fff;
}

.movie-header-title {
  margin-bottom: 0;
  line-height: 1.5;
  font-size: 1.25rem;
}

.movie-info-content {
  padding: 1rem;
  background-color: #fff;
}

.movie-poster-wrap {
  position: relative;
  padding-bottom: 150%;
  border-radius: 5px;
  overflow: hidden;
  transition: all 0.2s ease;
}

.movie-poster {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-size: cover;
  background-position: center center;
}

.movie-title {
  font-size: 3.5rem;
  font-weight: 300;
  line-height: 1.2;
}

.movie-rating {
  padding: 0;
}

.movie-rating:focus {
  box-shadow: none;
}

.movie-rating >>> .b-rating-star,
.movie-rating >>> .b-rating-value {
  justify-content: flex-start;
  flex-grow: 0 !important;
  font-size: 1.3rem;
  font-weight: 300;
  padding: 0;
}

.movie-description {
  font-size: 1.25rem;
  font-weight: 300;
}

.close-icon {
  font-size: 24px;
  cursor: pointer;
}
</style>