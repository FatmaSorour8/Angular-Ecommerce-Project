import { Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { AboutComponent } from './Pages/about/about.component';
import { ContactComponent } from './Pages/contact/contact.component';
import { RegisterComponent } from './Pages/register/register.component';
import { LoginComponent } from './Pages/login/login.component';
import { AdminDashboardComponent } from './Pages/admin-dashboard/admin-dashboard.component';
import { NotfoundComponent } from './Pages/notfound/notfound.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { ForgetPasswordComponent } from './Pages/forget-password/forget-password.component';
import { ResetPasswordComponent } from './Pages/reset-password/reset-password.component';
import { ProductDetailsComponent } from './Pages/products/product-details/product-details.component';
import { ProductsComponent } from './Pages/products/products.component';
import { AdminProductsComponent } from './Pages/admin-products/admin-products.component';
import { AdminProductFormComponent } from './Pages/admin-products/admin-product-form/admin-product-form.component';
import { AdminCategoryComponent } from './Pages/admin-category/admin-category.component';
import { CategoryFormComponent } from './Pages/admin-category/category-form/category-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'adminproducts', component: AdminProductsComponent },
  { path: 'products/:id', component: ProductDetailsComponent },
  { path: 'product', component: ProductsComponent },
  { path: 'adminproducts/:id/edit', component: AdminProductFormComponent },
  { path: 'admincategory', component: AdminCategoryComponent },
  { path: 'admincategory/:id/edit', component: CategoryFormComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'forgetpass', component: ForgetPasswordComponent },
  { path: 'resetpass', component: ResetPasswordComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'admin', component: AdminDashboardComponent },

  { path: '**', component: NotfoundComponent },
];