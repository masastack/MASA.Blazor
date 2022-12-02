# Border radius（边框半径）

使用边框半径辅助样式对元素快速应用 `border-radius` 样式

## 使用

使用示例如下所示。注意：中缀字符 sm、lg 和 xl 与帧半径大小有关，不受断点影响。

<masa-example file="Examples.styles_and_animations.border_radius.Basic"></masa-example>

## 示例

### 其他

#### 药丸和圆形

您可以使用 `.rounded-pill` 类创建药丸，使用 `.rounded-circle` 类创建圆圈。

<masa-example file="Examples.styles_and_animations.border_radius.Round"></masa-example>

#### 移除边框半径

使用 `.rounded-0` 辅助类删除元素的所有半径或角选择的半径；例如：`.rounded-l-0` 和 .`rounded-tr-0`。

<masa-example file="Examples.styles_and_animations.border_radius.Remove"></masa-example>

#### 设置所有角的半径

**rounded** 辅助类允许您修改元素的边框半径。使用 `.rounded-sm`、`.rounded`、.`rounded-lg` 和 `.rounded-xl` 添加不同大小的边框半径。

<masa-example file="Examples.styles_and_animations.border_radius.Set"></masa-example>

#### 通过边设置半径

可以使用 t, r, b, l 内置类在每一边配置边框半径；例如：`.rounded-b-xl` 和 `.rounded-t`。

<masa-example file="Examples.styles_and_animations.border_radius.Side"></masa-example>

#### 通过角设置半径

边框半径可以通过使用 tl, tr, br, bl 内置类在每个角上配置; 例如：`.rounded-br-xl`和 `.rounded-tr`。

<masa-example file="Examples.styles_and_animations.border_radius.Horn"></masa-example>