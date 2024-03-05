<template>
    <div class="row">
        <div class="col-xl-4 col-12">

            <div class="card shadow">


                <div class="card-header p-0 d-flex justify-content-between">

                    <div class="align-self-center">
                        <span class="text-dark fw-bold ms-3">Classifications</span>
                    </div>

                    <div class="card-action">
                        <button data-bs-toggle="modal" data-bs-target="#action-model-modal" @click="OpenAction()"
                                class="btn btn-outline-dark px-3 border-0 rounded-0 rounded-top-3 d-flex align-items-center">
                            <span class="btn-inner mx-1">
                                <i class="bi bi-plus-square"></i>
                            </span>
                            {{'New'}}
                        </button>
                    </div>
                </div>



                <div class="card-body p-0" style="min-height:9rem">



                    <div class="table-responsive" style="max-height:70vh;">
                        <table class="table table-hover w-100">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Color</th>
                                    <th class="text-center">Actions</th>
                                </tr>
                            </thead>
                            <tbody>


                                <tr v-for="item in List" :key="item">
                                    <td>{{item.name}}</td>
                                    <td>{{item.color}}</td>

                                    <td class="d-flex justify-content-center">
                                        <button class="btn btn-icon btn-soft-light ms-3 btn-sm border-0"
                                                @click="OpenAction(item.id)"
                                                data-bs-toggle="modal" data-bs-target="#action-model-modal">
                                            <span class="btn-inner">
                                                <i class="bi bi-pencil-square"></i>
                                            </span>
                                        </button>

                                        <button class="btn btn-icon btn-soft-danger ms-3 btn-sm border-0"
                                                @click="OpenAction(item.id!)"
                                                data-bs-toggle="modal" data-bs-target="#delete-model-modal">
                                            <span class="btn-inner">
                                                <i class="bi bi-trash3 text-danger"></i>
                                            </span>
                                        </button>
                                    </td>

                                </tr>


                            </tbody>
                        </table>
                    </div>
                </div>


                <div class="card-footer py-1">
                   
                    <!--
                        no time to in 2h 
                        <Paginator :List="List" orientation v-model:entitiesChange="pageList" />-->

                </div>

            </div>
        </div>
    </div>




    <div class="modal fade" id="action-model-modal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{isAdd?'Add':'Edit' }} Classification</h5>
                </div>
                <form class="was-validated" @submit.prevent="Action">
                    <div class="modal-body">
                        <div class="form-group mb-2">
                            <input class="form-control" autocomplete="none" id="name"
                                   placeholder="name" required="" type='text' v-model.trim="_model.name"
                                   maxlength="60"
                                   name="name" />
                            <!--with trim no need any more-->
                            <div class="text-danger" v-if="(_model.name != null && _model.name!.startsWith(' '))">
                                {{'whitespace invalid to start text' }}
                            </div>
                        </div>

                        <div class="form-group mb-2">
                            <select class="form-control" required v-model="_model.color">
                                <!--<option value="undefined" selected disabled>select</option>-->
                                <!--can be iter of enum from object key...-->
                                <option value="0">White</option>
                                <option value="1">LightCyan</option>
                                <option value="2">LightPink</option>
                                <option value="3">LightYellow</option>
                            </select>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-dark">
                            {{ isAdd?'Add':'Edit' }}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>




    <div class="modal fade" id="delete-model-modal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content" style="max-height:50%">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Classification</h5>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table align-items-center table-flush" role="grid">
                            <thead class="thead-light">
                                <tr>
                                    <th>Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="d-flex align-items-center">
                                            <h6>{{_model.name}}</h6>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    {{'Please ensure then take action!'}}
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <button type="submit" class="btn btn-danger" data-bs-dismiss="modal" @click="Delete()">Delete</button>
                </div>

            </div>
        </div>
    </div>



</template>

<script setup lang="ts">

    import { onMounted, ref, computed } from 'vue';
    import { useStore } from "vuex";
    import { ClassificationDto, Colors } from '../core/models/ClassificationDto';
    import  Paginator  from '../components/Paginator.vue';

    const store = useStore()
    

   // const isLoaded = ref<boolean>(true);
    const isAdd = ref<boolean>(true);
    const _model = ref<ClassificationDto>({});
    

    onMounted(() => {
        store.dispatch("classification/Call_ClassificationList");
    });

    const List = computed(() => {
        return store.getters['classification/Get_ClassificationList'] ?? []
    })

    



    function OpenAction(id?: string | null) {

        isAdd.value = id ? false : true;

        if (isAdd.value) {
            _model.value = {};
        } else { //edit / delete
            _model.value = { ...List.value.find(x => x.id == id)! };
        }
    }

    function Action() {
        if (isAdd.value) {
            _model.value.color = Number(_model.value.color)
            store.dispatch("classification/Call_ClassificationAdd", _model.value).then(() => {
                _model.value = {};
            });
        } else {
            _model.value.color = Number(_model.value.color)
            store.dispatch("classification/Call_ClassificationEdit", _model.value);

        }
    }



    function Delete() {

        if (_model.value.id) {
            store.dispatch("classification/Call_ClassificationDelete", _model.value.id).then(() => {
                _model.value = {};
            });
        }
    }


</script>

<style scoped>
.custom-table {
/* min-width: 900px; */
}

.custom-table thead tr, .custom-table thead th {
    border-top: none;
    border-bottom: none !important;
}

.custom-table tbody th, .custom-table tbody td {
    color: #777;
    font-weight: 400;
    padding-bottom: 20px;
    padding-top: 20px;
}

    .custom-table tbody th small, .custom-table tbody td small {
        color: #b3b3b3;
        font-weight: 300;
    }

.custom-table tbody tr:not(.spacer) {
    border-radius: 7px;
    -o-transition: .3s all ease;
}

    .custom-table tbody tr:not(.spacer):hover {
        -webkit-box-shadow: 0 2px 10px -5px rgba(0, 0, 0, 0.1);
        box-shadow: 0 2px 10px -5px rgba(0, 0, 0, 0.1);
    }

.custom-table tbody tr th, .custom-table tbody tr td {
    background: #fff;
    border: none;
}

    .custom-table tbody tr th:first-child, .custom-table tbody tr td:first-child {
        border-top-left-radius: 7px;
        border-bottom-left-radius: 7px;
    }

    .custom-table tbody tr th:last-child, .custom-table tbody tr td:last-child {
        border-top-right-radius: 7px;
        border-bottom-right-radius: 7px;
    }

.custom-table tbody tr.spacer td {
    padding: 0 !important;
    height: 10px;
    border-radius: 0 !important;
    background: transparent !important;
    pointer-events: none
}
</style>