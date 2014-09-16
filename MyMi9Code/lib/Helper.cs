using MyMi9Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMi9Code.lib
{
    public class Helper
    {
        private static Helper _instance;

        private Helper()
        {
        }

        public readonly string jsonRequest = @"{
	        'type':'object',
	        'properties':{
		        'payload': {
			        'type':'array',
			        'id': 'http://jsonschema.net/payload',
			        'required':false,
			        'items':
				        {
					        'type':'object',
					        'id': 'http://jsonschema.net/payload/0',
					        'required':false,
					        'properties':{
						        'country': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/country',
							        'required':false
						        },
						        'description': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/description',
							        'required':false
						        },
						        'drm': {
							        'type':'boolean',
							        'id': 'http://jsonschema.net/payload/0/drm',
							        'required':false
						        },
						        'episodeCount': {
							        'type':'number',
							        'id': 'http://jsonschema.net/payload/0/episodeCount',
							        'required':false
						        },
						        'genre': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/genre',
							        'required':false
						        },
						        'image': {
							        'type':'object',
							        'id': 'http://jsonschema.net/payload/0/image',
							        'required':false,
							        'properties':{
								        'showImage': {
									        'type':'string',
									        'id': 'http://jsonschema.net/payload/0/image/showImage',
									        'required':false
								        }
							        }
						        },
						        'language': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/language',
							        'required':false
						        },
						        'nextEpisode': {
							        'type':'null',
							        'id': 'http://jsonschema.net/payload/0/nextEpisode',
							        'required':false
						        },
						        'primaryColour': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/primaryColour',
							        'required':false
						        },
						        'seasons': {
							        'type':'array',
							        'id': 'http://jsonschema.net/payload/0/seasons',
							        'required':false,
							        'items':
								        {
									        'type':'object',
									        'id': 'http://jsonschema.net/payload/0/seasons/0',
									        'required':false,
									        'properties':{
										        'slug': {
											        'type':'string',
											        'id': 'http://jsonschema.net/payload/0/seasons/0/slug',
											        'required':false
										        }
									        }
								        }
							

						        },
						        'slug': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/slug',
							        'required':false
						        },
						        'title': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/title',
							        'required':false
						        },
						        'tvChannel': {
							        'type':'string',
							        'id': 'http://jsonschema.net/payload/0/tvChannel',
							        'required':false
						        }
					        }
				        }
			

		        },
		        'skip': {
			        'type':'number',
			        'id': 'http://jsonschema.net/skip',
			        'required':false
		        },
		        'take': {
			        'type':'number',
			        'id': 'http://jsonschema.net/take',
			        'required':false
		        },
		        'totalRecords': {
			        'type':'number',
			        'id': 'http://jsonschema.net/totalRecords',
			        'required':false
		        }
	        }
        }";

        public readonly string jsonResopnse = @"{'type':'object','properties':{ 'response': { 'type':'array', 'id': 'http://jsonschema.net/response', 'required':false, 'items': { 'type':'object', 'id': 'http://jsonschema.net/response/0', 'required':false, 'properties':{ 'image': { 'type':'string', 'id': 'http://jsonschema.net/response/0/image', 'required':false }, 'slug': { 'type':'string', 'id': 'http://jsonschema.net/response/0/slug', 'required':false }, 'title': { 'type':'string', 'id': 'http://jsonschema.net/response/0/title', 'required':false } } } } }}";

        // lockObject
        //private static object lockObject = new object();

        public static Helper Instance()
        {
            if (_instance == null)
            {
                _instance = new Helper();
            }

            //For Multiful thread
            //if (_instance == null)
            //{
            //  lock(lockObject)
            //  {
            //      _instance = new Helper();
            //  }
            //}

            return _instance;
        }


        public ResponseData GetResponseData(RequestData requestData)
        {
            var responseDatas = new ResponseData();
            foreach (Payload p in requestData.payload)
            {
                if (p.drm == true && p.episodeCount > 0)
                {
                    if (responseDatas.response == null)
                    {
                        responseDatas.response = new List<Response>();
                    }
                    responseDatas.response.Add(new Response() { image = p.image.showImage, slug = p.slug, title = p.title });
                }
            }
            return responseDatas;
        }
    }
}