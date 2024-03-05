<template>
    <div class="row">
        <div class="card-action">
            <button data-bs-toggle="modal" data-bs-target="#action-model-modal" @click="OpenAction()"
                    class="btn btn-outline-dark justify-content-center px-3 border-0 rounded-0 rounded-top-3 d-flex align-items-center">
                <span class="btn-inner mx-1">
                    <i class="bi bi-plus-square"></i>
                </span>
                {{'New'}}
            </button>
        </div>
    </div>
    <div class="row">

        <div class="col-md-3 col-sm-1" v-for="item in List" :key="item" >


            <!--change color of card as  classification color-->

            <div class="card text-bg-light mb-3" >
                <!--time ago .. no time to do it-->
                <div class="card-header">5 days ago</div>
                <div class="card-body">
                    <h5 class="card-title">{{item.title}}</h5>
                    <p class="card-text">{{item.body}}</p>
                </div>
                <div class="card-footer">
                    <small> {{classificationLookup(item.classificationId)}}</small>
                    <div class="float-end">

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
                    </div>

                </div>

            </div>

        </div>




    </div>




    <div class="modal fade" id="action-model-modal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{isAdd?'Add':'Edit' }} Note</h5>
                </div>
                <form class="was-validated" @submit.prevent="Action">
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group mb-2">
                                <input class="form-control" autocomplete="none" id="name"
                                       placeholder="name" required="" type='text' v-model.trim="_model.title"
                                       maxlength="60"
                                       name="name" />
                                <!--with trim no need any more-->
                                <div class="text-danger" v-if="(_model.title != null && _model.title!.startsWith(' '))">
                                    {{'whitespace invalid to start text' }}
                                </div>
                            </div>

                            <div class="form-group mb-2">
                                <select class="form-control" v-model="_model.classificationId">
                                    <option v-for="cl in ClassificationList" :value="cl.id">{{cl.name}}</option>
                                </select>
                            </div>

                        </div>

                        <div class="row">
                            <div class="form-group mt-2">
                                <textarea class="form-control" placeholder="write note"  v-model="_model.body"
                                          id="bodynote" style="height: 100px"></textarea>
                            </div>
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
                                            <h6>{{_model.title}}</h6>
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
    import { NoteDto } from '../core/models/NoteDto';
    import { ClassificationDto } from '../core/models/ClassificationDto';

    const store = useStore()


   // const isLoaded = ref<boolean>(true);
    const isAdd = ref<boolean>(true);
    const _model = ref<NoteDto>({});


    onMounted(() => {
        store.dispatch("classification/Call_ClassificationList");
        store.dispatch("note/Call_NoteList");
    });

    const List = computed(() => {
        return store.getters['note/Get_NoteList'] ?? []
    })

    const ClassificationList = computed<ClassificationDto[]>(() => {
        return store.getters['classification/Get_ClassificationList'] ?? []
    }) 

    const classificationLookup = (id: string) =>
        ClassificationList.value.find(x => x.id == id)?.name;
    


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
            store.dispatch("note/Call_NoteAdd", _model.value).then(() => {
                _model.value = {};
            });
        } else {
            store.dispatch("note/Call_NoteEdit", _model.value);

        }
    }



    function Delete() {

        if (_model.value.id) {
            store.dispatch("note/Call_NoteDelete", _model.value.id).then(() => {
                _model.value = {};
            });
        }
    }


</script>

<style scoped>
    .card-footer ,
    .card-header{
        background-color:unset !important
    }
</style>