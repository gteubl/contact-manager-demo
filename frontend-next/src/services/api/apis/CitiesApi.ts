/* tslint:disable */
/* eslint-disable */
/**
 * ContactManagerDemo.RestApi
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  CityDto,
} from '../models/index';
import {
    CityDtoFromJSON,
    CityDtoToJSON,
} from '../models/index';

/**
 * 
 */
export class CitiesApi extends runtime.BaseAPI {

    /**
     */
    async apiCitiesCitiesGetRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<CityDto>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            // oauth required
            headerParameters["Authorization"] = await this.configuration.accessToken("oauth2", []);
        }

        const response = await this.request({
            path: `/api/Cities/cities`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(CityDtoFromJSON));
    }

    /**
     */
    async apiCitiesCitiesGet(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<CityDto>> {
        const response = await this.apiCitiesCitiesGetRaw(initOverrides);
        return await response.value();
    }

}
