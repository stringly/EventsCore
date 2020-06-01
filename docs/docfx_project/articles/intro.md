# Introduction

I've had several abortive attempts at implementing an Event Scheduling/Management solution that can cover all of the use cases that I've been asked to automate over the last few years. It is easily the most common type of request that I get: someone needs to run a training program, and they need a way for people to be able to schedule to attend. Most of the time they are using some hellish system involving a spreadsheet that they email back and forth.

These requests come to me informally, usually from some poor person who has inherited a nightmarish task and is under a serious deadline. I always do the same thing: I slap something together as quickly as I can using whatever technology I can get my hands on. I've implemented some version of this functionality in Google Apps Script or Google Forms or an Access Database that talks to a Google sheet or some other filthy hack that works just enough for them to love it and then ask me to keep it alive month after month. 

I spend way too much of my time paying for my past choices. I need one, well-designed (or at least not *terribly* designed) solution that can accomodate all of the requirements in the various and scattered solutions I've slapped together in the past. That's the primary goal of this project.

The second goal is to attempt to apply some sound architecture principals to the structure of the project. In the past, I've been too liberal in allowing myself to ignore documentation, unit/integration testing, and proper encapsulation, in an effort to "just get it working" or show a proof of concept. The inevitable result is that the proof of concept version becomes the definitive version, and I am forced to update and maintain a project that I can't make sense of even a month later. I know that applying sound principles will significantly delay deploying a working version, but I can't keep making the same poor choices.




