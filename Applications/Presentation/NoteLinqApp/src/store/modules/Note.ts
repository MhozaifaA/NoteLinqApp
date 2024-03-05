import HttpAppService from '../../core/services/http.service';
import { noteUrls } from "../../core/appsettings"
import type { NoteDto } from '../../core/models/NoteDto';

export default {
    namespaced: true,
    state: () => {
        NoteList: []
    },
    getters: {
        Get_NoteList: (state: any) => state.NoteList || [],
    },
    actions: {
        Call_NoteList({ commit }: any) {
            (HttpAppService._Get<NoteDto[]>
                (noteUrls.ListUrl)).
                Result(data => {
                    commit("Set_NoteList", data);
                }).Error(err => console.error(err));
        },

        Call_NoteAdd({ commit }: any, dto: NoteDto) {
            (HttpAppService._Post<NoteDto>
                (noteUrls.AddUrl, dto)).
                Result(data => {

                    commit("Push_NoteList", data);

                }).Error(err => console.error(err));
        },


        Call_NoteEdit({ commit }: any, dto: NoteDto) {
            (HttpAppService._Put<NoteDto>
                (noteUrls.ModifyUrl, dto)).
                Result(data => {

                    commit("Modify_NoteList", data);

                }).Error(err => console.error(err));
        },

        Call_NoteDelete({ commit }: any, id: string) {
            (HttpAppService._Delete<NoteDto>
                (noteUrls.DeleteUrl + '/' + id)).
                Result(data => {

                    commit("Remove_NoteList", data);

                }).Error(err => console.error(err));
        },
    },
    mutations: {
        Set_NoteList(state: any, list: NoteDto[]) {
            state.NoteList = list;
        },
        Push_NoteList(state: any, dto: NoteDto) {
            state.NoteList.unshift(dto);// = [dto!, ...this.List];
        },

        Modify_NoteList(state: any, dto: NoteDto) {
            let indexlis = state.NoteList.findIndex((x: NoteDto) => x.id == dto.id)!;
            state.NoteList[indexlis] = dto!;
        },
        Remove_NoteList(state: any, id: string) {
            //fix error
            let indexlis = state.NoteList.findIndex((x: NoteDto) => x.id == id)!;
            state.NoteList.splice(indexlis, 1);
            //state.NoteList = state.NoteList.filter((x: NoteDto) => x.id != id);// [...state.NoteList];
        },
    },
}