# Drone Simulator & BCI Project

### Project Purpose:
This project aims to explore ways to overcome the brain's non-stationary dynamics.  
Our goal is to collect EEG data when an individual thinks about moving their left or right arm.  

The system consists of:  
- A **Drone Simulator** developed in Unity  
- A [**Unicorn Black Suite BCI**](https://www.gtec.at/product/unicorn-hybrid-black/?srsltid=AfmBOopszlZHrlDeKNcj01YF5Bgfuk_F8UhkEYQ_KhfeXSD-79rVRNXM)

Network Details: Uses a socket connection with the BCI, handled by the script UdpConnection.  

### Simple Explanation:
We want to capture brain activity at the beginning of motor intention.  

1. The drone moves autonomously.  
2. Before reaching an obstacle, an arrow appears to trigger the thought process.  
3. Shortly after, the drone automatically strafes in the required direction.  
4. We analyze the relationship between brain activity and the computer's response.  
5. Our end goal is to train models to detect movement intention and control the drone using imagination alone (using EEG).  

### Data Acquisition:
We use Python for data processing.  
[MI-Data GitHub Repo](https://github.com/bci4cpl/MI-Data.git)  


### Current Prototype (Feb 17th):
This dataset will be used to train our machine learning models.  

*For more details: Refer to the "ProtocolAndNotes" script.  
