<template>
    <div id="app">
      <v-app>
            <v-dialog v-model="dialog" persistent max-width="600px" min-width="360px">
                <div>
                    <v-card>
                        <v-tabs
                            v-model="tab"
                            color="deep-purple-accent-4"
                            align-tabs="center"
                        >
                            <v-tab :value="Login" @click="setTab('Login')">
                                <v-icon large>mdi-account</v-icon>
                                <div class="caption py-1">Đăng nhập</div>
                            </v-tab>
                            <v-tab :value="Register" @click="setTab('Register')">
                                <v-icon large>mdi-account-outline</v-icon>
                                <div class="caption py-1">Đăng ký</div>
                            </v-tab>
                        </v-tabs>
                        <v-window v-model="tab" >
                            <v-window-item
                                value="Login"
                            >
                                <v-tab-item>
                                    <v-card>
                                        <v-card-text>
                                            <v-form v-model="valid" @submit.prevent="Login()">
                                                <v-row>
                                                    <v-col cols="12">
                                                        <v-text-field v-model="username"  label="Tài khoản" ></v-text-field>
                                                    </v-col>
                                                    <v-col cols="12">
                                                        <v-text-field v-model="password" 
                                                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" 
                                                        :type="show1 ? 'text' : 'password'" 
                                                        label="Mật khẩu" hint="Ít nhất 8 ký tự" 
                                                        :rules="[() => !!password || 'Mật khẩu không được trống']"
                                                        @click:append="show1 = !show1"></v-text-field>
                                                    </v-col>
                                                    <v-spacer></v-spacer>
                                                    <v-col class="d-flex" cols="12" align-end >
                                                        <v-btn class="ml-15" x-large style="width:120px" @click="$emit('close')" color="error">  Hủy </v-btn>
                                                        <v-spacer></v-spacer>
                                                        <v-btn class="mr-15" x-large :disabled="!valid" color="success" type="submit"> Đăng nhập </v-btn>
                                                    </v-col>
                                                </v-row>
                                            </v-form>
                                        </v-card-text>
                                    </v-card>
                                </v-tab-item>
                        </v-window-item>
                        <v-window-item 
                            value="Register"
                        >
                            <v-tab-item>
                                <v-card class="px-4">
                                    <v-card-text>
                                        <v-form ref="registerForm" v-model="valid" lazy-validation @submit.prevent="Register()">
                                            <v-row>
                                                <v-col cols="12">
                                                    <v-text-field 
                                                        v-model="user.fullName" 
                                                        label="Học và tên" 
                                                        maxlength="20" 
                                                        required
                                                    ></v-text-field>
                                                    <v-text-field 
                                                        v-model="user.username" 
                                                        label="Tài khoản" 
                                                        maxlength="20" 
                                                        required
                                                    ></v-text-field>
                                                </v-col>
                                                <v-col cols="12">
                                                    <v-text-field
                                                        v-model="user.email"  
                                                        label="E-mail" 
                                                        required
                                                    ></v-text-field>
                                                </v-col>
                                                <v-col cols="12">
                                                    <v-text-field 
                                                        v-model="user.password"
                                                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" 
                                                        :type="show1 ? 'text' : 'password'"  
                                                        label="Mật khẩu" 
                                                        hint="Ít nhất 8 ký tự"  
                                                        @click:append="show1 = !show1"
                                                        :rules="[() => !!user.password || 'Mật khẩu không được trống']"
                                                    ></v-text-field>
                                                </v-col>
                                                <v-col cols="12">
                                                    <v-text-field 
                                                        v-model="confirmPassword"
                                                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" 
                                                        :type="show1 ? 'text' : 'password'" 
                                                        label="Nhập lại mật khẩu"  
                                                        @click:append="show1 = !show1"
                                                        :rules="[() => !!confirmPassword || 'Nhập lại mật khẩu không được trống', () => confirmPassword === user.password || 'Mật khẩu không khớp']"
                                                     ></v-text-field>
                                                </v-col>
                                                <v-col cols="12">
                                                    <v-text-field  
                                                        v-model="user.address"
                                                        label="Địa chỉ" 
                                                        maxlength="20" 
                                                        required
                                                    ></v-text-field>
                                                </v-col>
                                                <v-col class="d-flex" cols="12">
                                                    <v-btn class="ml-15" x-large style="width:120px" @click="$emit('close')" color="error">  Hủy </v-btn>
                                                    <v-spacer></v-spacer>
                                                    <v-btn class="mr-15" x-large :disabled="!valid" color="success" type="submit">Đăng ký</v-btn>
                                                </v-col>
                                            </v-row>    
                                        </v-form>
                                    </v-card-text>
                                </v-card>
                            </v-tab-item>
                            </v-window-item>
                        </v-window>
                    </v-card>
                </div>
            </v-dialog>
        </v-app>
    </div>
</template>
  
  <script>
  export default {
    props:['dialogLogin'],
    computed:{
        dialog:{
            get(){
                return this.dialogLogin;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        },
    },
    data(){
    return{
        tab: null,
        show1: false,
        valid:true,
        username:'',
        password:'',
        user:{
            id: 0,
            fullName:'',
            username:'',
            email:'',
            password:'',
            address:'',
            status:1,
        },
        confirmPassword:'',
    }
    },
    methods:{
        Register(){
            this.$store.dispatch("addUser", this.user)
            this.rerestdata();
            this.dialog=false
        },
        Login(){
            this.$store.dispatch('login', {
                username: this.username,
                password: this.password,
            });
            this.rerestdata();
            console.log()
            this.dialog=false;
            
        },
        rerestdata(){
            this.username = '';
            this.password = '';
        },
        setTab(tabName) {
            this.tab = tabName;
        },
        rerestdata(){
            this.user.id= 0;
            this.user.fullName='';
            this.user.username='';
            this.user.email='';
            this.user.password='';
            this.user.address='';
            this.user.status=1;
            this.username='';
            this.password='';
            this.valid=true;
            this.confirmPassword='';
        }

    },
  }
  </script>
  
  <style>
  
  </style>