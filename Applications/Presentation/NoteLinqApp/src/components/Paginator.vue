
<template>
    <div class="text-center" v-if="List.length == 0">
        <label class="mb-3 align-self-center text-secondary">
            {{'not data'}}
        </label>
    </div>
    <div v-if="List.length > 0" :class="{ 'd-flex w-100  justify-content-between': orientation }" class="text-center" >


        <label class="align-self-center">
            {{'show'}}
            <select class="form-select-sm mb-3 p-1 py-2 rounded-3"
                    @change="SelectedQuantity($event)">
                <option value="1">1</option>
                <option value="8">8</option>
                <option selected value="12">12</option>
                <option value="20">20</option>
                <option value="32">32</option>
                <option value="54">54</option>
                <option value="100">100</option>
                <option :value="Total">{{'All'}}</option>
            </select>
            {{'entries of'}} <span class="mx-1">{{Total}}</span>
        </label>

        <nav class="mx-1">

            <ul class="pagination justify-content-center pg-dark">

                <li v-for="(link,i) in links"
                    @click="SelectedPageInternal(link)">
                    <button class="{{(i<=1 || i+2 >= links.length)? 'pagefont btn-soft-light' : (link.active?'mx-1 '+Color:'mx-1 btn-light' ) }}
              btn btn-icon border-0 " :disabled="!link.enabled">
                        {{link.text}}
                    </button>
                </li>

            </ul>



        </nav>

        <label class="mb-3 align-self-center">
            {{'page'}} {{CurrentPage}} {{'of'}} {{TotaPagesQuantity}}
        </label>

    </div>
</template>


<script setup lang="ts">
    import { computed, onMounted, onUpdated, ref, watch } from 'vue';


    export interface LinkModel {
        text?: string | null;
        page: number;
        enabled?: boolean;
        active?: boolean;
    }
    const emit = defineEmits(['update:modelValue', 'SelectedPage'])
    const prop = defineProps({
        orientation: Boolean,
        List: { type: Array, default: [], required: true },
        color: { type: String, default: 'btn-primary' },
    })
    
    const CurrentPage = ref<number>(1)
    const Total = ref<number>(0)
    const Radius = ref<number>(2)
    const Quantity = ref<number>(12)
    const links = ref<LinkModel[]>([])
    CurrentPage.value = 10;
    onMounted(() => {
       
        CurrentPage.value = 1;
        Total.value = prop.List.length;
        LoadPages();
    })
    watch(prop, () => {
        CurrentPage.value = 1;
        Total.value = prop.List.length;
        LoadPages();
    })


    const TotaPagesQuantity = computed(() => {
        return Math.ceil(Total.value / Quantity.value)
    })

    function SelectedQuantity(event: any) {
        console.log(event.target.value)
        CurrentPage.value = 1;
        Quantity.value = Number(event.target.value);
        LoadPages();
        emit('SelectedPage', { page: CurrentPage.value, quantity: Quantity.value });
    }

    function SelectedPageInternal(link: LinkModel) {
        if (link.page == CurrentPage.value)
            return;

        if (!link.enabled)
            return;

        if (TotaPagesQuantity.value == 0)
            return;

        CurrentPage.value = link.page;
        emit('SelectedPage', { page: CurrentPage, quantity: Quantity });
        LoadPages();
    }

    function LoadPages() {
        links.value = [];
        let isPreviousPageLinkEnabled = CurrentPage.value != 1;
        let previousPage = CurrentPage.value - 1;

        links.value.push({ page: 1, enabled: isPreviousPageLinkEnabled, text: "«" });

        links.value.push({ page: previousPage, enabled: isPreviousPageLinkEnabled, text: "‹" });

        for (let i: number = 1; i <= TotaPagesQuantity.value; i++) {
            if (i >= CurrentPage.value - Radius.value && i <= CurrentPage.value + Radius.value) {
                links.value.push({ page: i, enabled: true, active: CurrentPage.value == i, text: i.toString() });
            }
        }

        var isNextPageLinkEnabled = CurrentPage != TotaPagesQuantity;
        var nextPage = CurrentPage.value + 1;

        links.value.push({ page: nextPage, enabled: isNextPageLinkEnabled, text: "›" });

        links.value.push({ page: TotaPagesQuantity.value, enabled: isNextPageLinkEnabled, text: "»" });

        let data = prop.List.slice((CurrentPage.value - 1) * Quantity.value, CurrentPage.value * Quantity.value);

        emit('update:modelValue', data);
    }


</script>

<style scoped>

    ul {
        list-style-type: none !important;
    }

    .btn-primary:hover,
    .btn-primary {
        --bs-btn-bg: var(--bs-dark);
    }

    .btn:hover {
        color: white;
        background-color: gray
    }
</style>