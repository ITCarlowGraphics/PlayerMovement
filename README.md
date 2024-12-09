# Custom Player Movement Package Documentation

## Overview

The **Custom Player Movement Package** provides a flexible system to define and implement customized movement behaviors for players in a board game environment. By allowing the integration of mathematical functions for movement, rotation, and scaling, it allows developers to create unique animations like hopping, flipping, slithering, and more.

---

## Key Features

- **Customizable Movement Behaviors:** Define specific behaviors such as hopping, flipping, and more using mathematical functions.
- **Dynamic Components:** Support for movement, rotation, and scaling on different axes.
- **Temporary Behaviors:** Set temporary behaviors for a specific number of moves.

---

## Example

### Sample Scene

Here we are testing the movement on a board game. The player moves through board spaces by entering the amount of spaces into the text box and clicking "Move".
You can also click next to select a different piece to move.

You can try out some of the preset behaviours by clicking on buttons labelled with "Behaviour". There are 5 presets to choose from.
#### 1. Hopping
#### 2. A "Big" Hop
#### 3. Front Flipping
#### 4. Side Flipping
#### 5. Slithering.

There are many customisations to be made, so continue to read to see how you can make your own cool behaviour!

---

## Getting Started

### 1. Initializing the Movement Controller

To set up the player movement, initialize the `MovementController` with a customizable behavior and movement duration.

```csharp
float movementDuration = 0.75f;
CustomisableBehaviour custom = new HopBehaviour();
movementController.SetBehaviour(custom);
movementController.SetMovementDuration(movementDuration);
```

### 2. Configuring Start and End Positions

Define the start and end positions for the movement:

```csharp
movementController.SetStartAndEnd(Vector3 start, Vector3 end);
```

### 3. Updating Movement

Pass the controlled entity's transform into the `Update` function to apply the necessary transformations:

```csharp
movementController.Update(currentTransform);
```

### 4. Checking Movement Completion

Use the `MovementComplete` function to determine if the movement is finished:

```csharp
if (movementController.MovementComplete()) {
    // Movement has completed
}
```

---

## Movement Controller Interface

### Core Functions

- **Set Behavior:** Assign a behavior instance to the controller.
  ```csharp
  public void SetBehaviour(CustomisableBehaviour behaviour);
  ```
- **Set Movement Duration:** Specify the duration of the movement.
  ```csharp
  public void SetMovementDuration(float movementDuration);
  ```
- **Define Start and End Positions:**
  ```csharp
  public void SetStartAndEnd(Vector3 start, Vector3 end);
  ```
- **Update Movement:** Apply transformations to the controlled entity’s transform.
  ```csharp
  public void Update(Transform currentTransform);
  ```
- **Check Completion:** Determine if the movement act is complete.
  ```csharp
  public bool MovementComplete() => isMovementComplete;
  ```

---

## Customizable Components

The package provides interfaces to define movement, rotation, and scaling behaviors via mathematical functions:

### Movement Component

Define movement functions for the axes:

```csharp
public void SetMovementFunction(string axis, Func<float, float> function);
```

- Axis options: `"forward"`, `"up"`, `"side"`

### Rotation Component

Define rotation functions for the axes:

```csharp
public void SetRotationFunction(string axis, Func<float, float> function);
```

- Axis options: `"pitch"`, `"roll"`, `"yaw"`

### Scale Component

Define scaling functions for the axes:

```csharp
public void SetScaleFunction(string axis, Func<float, float> function);
```

- Axis options: `"x"`, `"y"`, `"z"`

---

## Defining Behaviors

Behaviors can be encapsulated in classes inheriting from `CustomisableBehaviour`. Below is an example of a **HopBehavior**:

### Hop Behavior Example

```csharp
public class HopBehaviour : CustomisableBehaviour
{
    public HopBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", Hop);
    }

    private float Hop(float progress)
    {
        const float maximumHopHeight = 1.3f;
        float hopOffset = Mathf.Sin(progress * Mathf.PI) * maximumHopHeight;
        return hopOffset;
    }
}
```

#### Explanation:

- **Forward Movement:** Linear interpolation between start and end positions.
- **Upward Movement:** A sine wave creates a hopping effect, where the `progress` determines the height.

---

### Combining Behaviors

Behaviors can combine movement, rotation, and scaling. For example, adding a front flip to the hop behavior:

```csharp
SetRotationFunction("pitch", SpeedUpContinueAndSlowDown);

public float SpeedUpContinueAndSlowDown(float progress)
{
    const float rotationSpeed = 360f;
    float angle = Mathf.Cos(progress * Mathf.PI) * rotationSpeed;
    return -angle;
}
```

- This creates a smooth flip effect by modifying the pitch rotation.

---

## Additional Examples

### Pulse Effect

Recreate a pulse effect by setting scale functions:

```csharp
public float Pulse(float progress)
{
    const float maxPulseSize = 1.2f;
    const float frequencyFactor = 0.5f;
    const float easingPower = 3f;

    float easedProgress = Mathf.Pow(progress, easingPower);
    float pulse = 1 + Mathf.Sin(easedProgress * Mathf.PI * 2 * frequencyFactor) * (maxPulseSize - 1);
    return pulse;
}

SetScaleFunction("x", Pulse);
SetScaleFunction("z", Pulse);
```

### Temporary Behaviors

Define temporary behaviors for specific moves:

```csharp
public void SetTemporaryBehaviour(CustomisableBehaviour behaviour, int numberOfTempMoves);
```

### Move to Distant Space

Use the `MoveToSpace` function to move to a space far away from the current position:

```csharp
public void MoveToSpace(int spaceNumber);
```

---

## Conclusion

The **Custom Player Movement Package** offers a flexible system to create dynamic player animations by leveraging mathematical functions. With its modular architecture, developers can easily define, combine, and customize behaviors to create unique behaviours.

