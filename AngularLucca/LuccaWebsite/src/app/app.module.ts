// Generic Angular

import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// App component

import { AppComponent } from './app.component';

// Routing module

import { AppRoutingModule } from './app-routing.module';

// Http

import { HttpClientModule } from '@angular/common/http';

// Forms

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Pages

import { HomeComponent, NotFoundComponent, DestinationComponent } from './pages';

// Shared components

import { ParallaxBackgroundComponent, DestinationInfosComponent, ActivityInfosComponent } from './shared/components';

// Angular material

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    DestinationComponent,
    ParallaxBackgroundComponent,
    DestinationInfosComponent,
    ActivityInfosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [HttpClientModule],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
