# An experiment to understand the effectiveness of culling and multithreading in the Unity Game Engine.

### Introduction:
In this experiment, we explored the impact of multithreading and rendering optimizations on the performance of a game environment using the Unity game engine. Specifically, we investigated two key areas of game performance: the effects of multithreaded computations on frame rate (FPS) and the role of distance-based culling in optimizing rendering workloads.

To evaluate the impact of multithreading, we implemented a custom agent wandering behavior, comparing single-threaded navigation using Unity’s built-in NavMeshAgent with a multithreaded alternative leveraging asynchronous tasks. For culling, we set up layer-based distance culling to reduce the rendering of objects beyond a specified range. Both approaches were tested under controlled conditions to analyze their effectiveness in improving overall performance, particularly in scenarios with a high computational or rendering load.

The findings of this experiment aim to provide insights into the practical benefits and limitations of these techniques, offering valuable guidance for game developers seeking to optimize their projects for better runtime efficiency.

### Findings:

#### Multithreading with NavMesh AI Agents:
Using two scripts found under Assets/Scripts/ `WanderBehaviour.cs` and `MultithreadedWanderer.cs` we took turns reviewing the performance of the a Windows Build on the following specs:

<br>
NVIDIA RTX 3060TI 12GB
<br>
AMD Ryzen 9 7900X3D
<br>
32GB 6000MT/s DDR5 Memory
<br>
Samsung Evo 970 Pro 2TB M.2 SSD
<br>

The `AgentSpawner.cs` script was used to generate AI agents with a parameter for quantity of agents instantiated. In the experiment, I observed differences the intervals: **10 - 100 - 200 - 500 - 1000**.

I found that multithreading did not provide a significant boost to the FPS often a difference of about 2-3 FPS at 1000 agents, however an interesting performance related observation was made. While multithreading, Many more of Unity's NavMesh agents can process scripts at once, this is immediately visible to the naked eye as early as 100 agents.

Below is a demonstration of the findings:

NavMesh Agents wandering around **with no Multithreading** at 1000 agents:
![Animation](https://github.com/user-attachments/assets/b81578c5-90f3-47dd-91d3-135d17358d3b)

NavMesh Agents wandering around **with Multithreading** at 1000 agents:
![Animation2-ezgif com-optimize](https://github.com/user-attachments/assets/d6f5d37b-80dc-4601-9c08-b10df02eaebe)

For culling I used a script to cull objects on the CullLayer layer when they are a fixed distance away from the camera. I included agents in this layer for the experiment.
The FPS gains were significant with culling enabled at 500+ agents often resulting in an increase between 10-12 FPS and at the extreme end with 3000 agents active I observed a 15-20 fps improvement, when paired with Multithreading you could see a difference of about 2-3 FPS which is attributed to the small FPS gains made by Multithreaded agents!

Below is a demonstration, you can view the difference by paying attention to the FPS counter on the top right of the gifs below:

**Culling Disabled:**
![Animation3](https://github.com/user-attachments/assets/e38a8555-6e65-46d8-a0c3-6a2bd7c351fa)

**Culling Enabled:**
![Animation4](https://github.com/user-attachments/assets/add6bdbe-b83e-43db-85d8-81435576c476)

