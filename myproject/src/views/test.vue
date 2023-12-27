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
                <v-tab :value="Login">
                  <v-icon large>mdi-account</v-icon>
                  <div class="caption py-1">Login</div>
                </v-tab>
                <v-tab :value="Login">
                  <v-icon large>mdi-account-outline</v-icon>
                  <div class="caption py-1">Register</div>
                </v-tab>
                </v-tabs>
                <v-window v-model="tab" >
                  <v-window-item
                    value="Login"
                  >
                  <v-tab-item>
                            <v-card>
                                <v-card-text>
                                    <v-form>
                                        <v-row>
                                            <v-col cols="12">
                                                <v-text-field  label="E-mail" ></v-text-field>
                                            </v-col>
                                            <v-col cols="12">
                                                <v-text-field :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :type="show1 ? 'text' : 'password'" label="Mật khẩu" hint="Ít nhất 8 ký tự" @click:append="show1 = !show1"></v-text-field>
                                            </v-col>
                                            <v-spacer></v-spacer>
                                            <v-col class="d-flex" cols="12" align-end >
                                                <v-btn class="ml-15" x-large @click="$emit('close')" colo="error">  Hủy </v-btn>
                                                <v-spacer></v-spacer>
                                                <v-btn class="mr-15" x-large :disabled="!valid" color="success" @click="validate"> Login </v-btn>
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
                                    <v-form ref="registerForm" v-model="valid" lazy-validation>
                                        <v-row>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field  label="Họ" maxlength="20" required></v-text-field>
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field  label="Tên" maxlength="20" required></v-text-field>
                                            </v-col>
                                            <v-col cols="12">
                                                <v-text-field  label="E-mail" required></v-text-field>
                                            </v-col>
                                            <v-col cols="12">
                                                <v-text-field  :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :type="show1 ? 'text' : 'password'"  label="Mật khẩu" hint="Ít nhất 8 ký tự"  @click:append="show1 = !show1"></v-text-field>
                                            </v-col>
                                            <v-col cols="12">
                                                <v-text-field :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :type="show1 ? 'text' : 'password'" label="Nhập lại mật khẩu"  @click:append="show1 = !show1"></v-text-field>
                                            </v-col>
                                            <v-col class="d-flex" cols="12">
                                                <v-btn class="ml-15" x-large @click="$emit('close')" colo="error">  Hủy </v-btn>
                                                <v-spacer></v-spacer>
                                                <v-btn class="mr-15" x-large :disabled="!valid" color="success" @click="validate">Register</v-btn>
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
    props:['dialogabout'],
    computed:{
          dialog:{
              get(){
                  return this.dialogabout;
              },
              set(value){
                  if(!value){
                      this.$emit('close');
                  }
              }
          }
      },

      data(){
        return{
            tab: null,
            show1: false,
        }
      }
  }
  </script>
  
  <style>
  
  </style>