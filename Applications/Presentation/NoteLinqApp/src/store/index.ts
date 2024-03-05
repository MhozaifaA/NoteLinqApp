import { createStore } from 'vuex'
import classification from './modules/Classification'
import note from './modules/Note'
import { classificationUrls } from "../core/appsettings"
import type { ClassificationDto } from '../core/models/ClassificationDto';
import HttpAppService from '../core/services/http.service';

//export default createStore({
//    state: () => {
//        ClassificationList: []
//    },
//    getters: {
//        Get_ClassificationList: (state: any) => state.ClassificationList,
//    },
//    actions: {
//         Call_ClassificationList({ commit }: any) {
//             HttpAppService._Get<ClassificationDto[]>
//                (classificationUrls.ListUrl).
//                Result(data => {
              
//                    commit("Set_ClassificationList", data);
//                }).Error(err => console.error(err));
//        },
//    },
//    mutations: {
//        Set_ClassificationList(state: any, list: ClassificationDto[]) {
//            state.ClassificationList = list;
//        },
//    },
//})





export default createStore({
    modules: {
        classification,
        note
    },
})