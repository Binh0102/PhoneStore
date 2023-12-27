<template>
    <div>
    <v-navigation-drawer 
    image="https://images.ctfassets.net/hrltx12pl8hq/5KiKmVEsCQPMNrbOE6w0Ot/341c573752bf35cb969e21fcd279d3f9/hero-img_copy.jpg?fit=fill&w=1200&h=630"
    permanent
    theme="dark">
        <v-list>
            <v-list-subheader>Danh muc</v-list-subheader>
            <v-list-item 
                v-for="(item, i) in links" 
                :key="i"
                :value="item"
                color="primary"
                :to="item.route">
                <template v-slot:prepend>
                    <v-icon class="mdi opacity-10" :icon="item.icon"></v-icon>
                </template>
                <v-list-item-title>{{ item.text }}</v-list-item-title>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>
    </div>
</template>

<script>
export default {
    name: 'SideBar',
    data(){
        return{
            links: [
                {icon: 'mdi-view-dashboard', text:'Thống kê',route:'/admin'},
                {icon: 'mdi-microsoft-windows', text:'Sản phẩm',route:'/products'},
                {icon: 'mdi-microsoft-windows', text:'Đơn hàng',route:'/order'},
                {icon: 'mdi-microsoft-windows', text:'Chi tiết đơn hàng',route:'/orderdetail'},
                {icon: 'mdi-microsoft-windows', text:'Người dùng',route:'/orderdetail'},
            ]
        }
    }
}
</script>

<style>
</style>