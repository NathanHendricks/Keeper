<template>
  <div class="modal fade" id="modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content form text-dark">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            <slot name="modal-title"></slot>
          </h5>
          <button type="button" class="btn-close btn-danger" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <slot name="modal-body"></slot>
        </div>
        <div class="modal-footer">
          <slot name="modal-footer" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>
.form{
    background-color: #ffffff80!important;
    box-shadow: 0px 0px 12px #b3aed7!important;
}
</style>