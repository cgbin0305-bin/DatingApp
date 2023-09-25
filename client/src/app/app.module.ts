import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // import for HttpClientModule
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [AppComponent, NavComponent, HomeComponent, RegisterComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // Enable for our to using Http Req in our application (Must manually import the library)
    BrowserAnimationsModule,
    FormsModule, // Import Forms to using Angular template form
    BsDropdownModule.forRoot(),
    /*
      FormsModule give us two way binding btw our component with the file html. Like we can send value from component to view, or view to component
    */
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
