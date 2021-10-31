<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="home-card p-5 bg-white rounded elevation-3">
      <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTC4IEOAzZA6LhLuVlcMyhupw_XSmRYZF_dLwsmPw_VgN5zAmOo5oHpLhv75UIbM0lVNOI&usqp=CAU" alt="Shadow Wolf Network Logo" class="rounded">
      <h1 class="my-5 bg-primary text-light p-3 rounded text-center">
        Welcome to the <b><em>Shadow Wolf Natework</em></b>
      </h1>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

<style scoped lang="scss">
.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
