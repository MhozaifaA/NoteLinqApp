import HttpAppService from '../../core/services/http.service';
import { classificationUrls } from "../../core/appsettings"
import type { ClassificationDto } from '../../core/models/ClassificationDto';

export default {
    namespaced: true,
    state: () => {
        ClassificationList: []
    },
    getters: {
        Get_ClassificationList: (state: any) => state.ClassificationList || [],
    },
    actions: {
         Call_ClassificationList({ commit }: any) {
            ( HttpAppService._Get<ClassificationDto[]>
                (classificationUrls.ListUrl)).
                Result(data => {
                    commit("Set_ClassificationList", data);
                }).Error(err => console.error(err));
        },

         Call_ClassificationAdd({ commit }: any, dto: ClassificationDto) {
            ( HttpAppService._Post<ClassificationDto>
                (classificationUrls.AddUrl, dto)).
                Result(data => {

                    commit("Push_ClassificationList", data);

                }).Error(err => console.error(err));
        },


        Call_ClassificationEdit({ commit }: any, dto: ClassificationDto) {
            (HttpAppService._Put<ClassificationDto>
                (classificationUrls.ModifyUrl, dto)).
                Result(data => {

                    commit("Modify_ClassificationList", data);

                }).Error(err => console.error(err));
        },

         Call_ClassificationDelete({ commit }: any, id: string) {
            ( HttpAppService._Delete<ClassificationDto>
                (classificationUrls.DeleteUrl + '/' + id)).
                Result(data => {
                   
                    commit("Remove_ClassificationList", data);

                }).Error(err => console.error(err));
        },
    },
    mutations: {
        Set_ClassificationList(state: any, list: ClassificationDto[]) {
            state.ClassificationList = list;
        },
        Push_ClassificationList(state: any, dto: ClassificationDto) {
            state.ClassificationList.unshift(dto);// = [dto!, ...this.List];
        },

        Modify_ClassificationList(state: any, dto: ClassificationDto) {
            let indexlis = state.ClassificationList.findIndex((x: ClassificationDto) => x.id == dto.id)!;
            state.ClassificationList[indexlis] = dto!;
        },
        Remove_ClassificationList(state: any, id: string) {

            let indexlis = state.ClassificationList.findIndex((x: ClassificationDto) => x.id == id)!;
            state.ClassificationList.splice(indexlis, 1);
            //state.ClassificationList = state.ClassificationList.filter((x: ClassificationDto) => x.id != id);// [...state.ClassificationList];
        },
    },
}